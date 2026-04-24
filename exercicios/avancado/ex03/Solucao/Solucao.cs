// Avançado 03 — Observer Pattern — SOLUÇÃO

interface IObservador<T> { void Atualizar(T evento); }

class EventoEstoque
{
    public string Produto { get; init; } = "";
    public int QuantidadeAtual { get; init; }
    public int QuantidadeMinima { get; init; }
}

class GerenciadorEstoque
{
    private readonly List<IObservador<EventoEstoque>> _observadores = new();
    private readonly Dictionary<string, (int Qtd, int Min)> _estoque = new();

    public void Assinar(IObservador<EventoEstoque> obs) => _observadores.Add(obs);
    public void Cancelar(IObservador<EventoEstoque> obs) => _observadores.Remove(obs);

    public void DefinirEstoque(string produto, int quantidade, int minimo)
    {
        _estoque[produto] = (quantidade, minimo);
    }

    public void RemoverDoEstoque(string produto, int quantidade)
    {
        if (!_estoque.ContainsKey(produto)) return;
        var (qtd, min) = _estoque[produto];
        var novaQtd = Math.Max(0, qtd - quantidade);
        _estoque[produto] = (novaQtd, min);
        Console.WriteLine($"Estoque de {produto}: {novaQtd} unidades");

        if (novaQtd <= min)
        {
            var evento = new EventoEstoque { Produto = produto, QuantidadeAtual = novaQtd, QuantidadeMinima = min };
            foreach (var obs in _observadores) obs.Atualizar(evento);
        }
    }
}

class ObservadorEmail : IObservador<EventoEstoque>
{
    public void Atualizar(EventoEstoque e) =>
        Console.WriteLine($"  [EMAIL] ALERTA: {e.Produto} com apenas {e.QuantidadeAtual} unidades! (mín: {e.QuantidadeMinima})");
}

class ObservadorSMS : IObservador<EventoEstoque>
{
    public void Atualizar(EventoEstoque e) =>
        Console.WriteLine($"  [SMS] Reposição necessária: {e.Produto} — atual: {e.QuantidadeAtual}");
}

class ObservadorDashboard : IObservador<EventoEstoque>
{
    private readonly List<string> _alertas = new();
    public void Atualizar(EventoEstoque e)
    {
        _alertas.Add($"{e.Produto}: {e.QuantidadeAtual}/{e.QuantidadeMinima}");
        Console.WriteLine($"  [DASHBOARD] {_alertas.Count} alerta(s) ativo(s)");
    }
}

class Programa
{
    static void Main()
    {
        var estoque = new GerenciadorEstoque();
        estoque.DefinirEstoque("Notebook", 10, 3);
        estoque.DefinirEstoque("Mouse", 20, 5);

        estoque.Assinar(new ObservadorEmail());
        estoque.Assinar(new ObservadorSMS());
        estoque.Assinar(new ObservadorDashboard());

        Console.WriteLine("=== Simulando vendas ===");
        estoque.RemoverDoEstoque("Notebook", 5);
        estoque.RemoverDoEstoque("Notebook", 4); // dispara alerta (1 unidade)
        estoque.RemoverDoEstoque("Mouse", 10);
    }
}

// Avançado 06 — Decorator Pattern — SOLUÇÃO

interface IProcessadorTexto { string Processar(string texto); }

class ProcessadorBase : IProcessadorTexto
{
    public string Processar(string texto) => texto;
}

abstract class DecoradorTexto : IProcessadorTexto
{
    protected readonly IProcessadorTexto _inner;
    protected DecoradorTexto(IProcessadorTexto inner) { _inner = inner; }
    public abstract string Processar(string texto);
}

class DecoradorMaiusculas : DecoradorTexto
{
    public DecoradorMaiusculas(IProcessadorTexto inner) : base(inner) { }
    public override string Processar(string texto) => _inner.Processar(texto).ToUpper();
}

class DecoradorRemoveEspacos : DecoradorTexto
{
    public DecoradorRemoveEspacos(IProcessadorTexto inner) : base(inner) { }
    public override string Processar(string texto) => _inner.Processar(texto).Trim();
}

class DecoradorTimestamp : DecoradorTexto
{
    public DecoradorTimestamp(IProcessadorTexto inner) : base(inner) { }
    public override string Processar(string texto) =>
        $"[{DateTime.Now:HH:mm:ss}] {_inner.Processar(texto)}";
}

class DecoradorContaPalavras : DecoradorTexto
{
    public DecoradorContaPalavras(IProcessadorTexto inner) : base(inner) { }
    public override string Processar(string texto)
    {
        var resultado = _inner.Processar(texto);
        int palavras = resultado.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        return $"{resultado} ({palavras} palavras)";
    }
}

class Programa
{
    static void Main()
    {
        // Composição de decorators
        IProcessadorTexto processador = new ProcessadorBase();
        processador = new DecoradorRemoveEspacos(processador);
        processador = new DecoradorMaiusculas(processador);
        processador = new DecoradorContaPalavras(processador);
        processador = new DecoradorTimestamp(processador);

        string entrada = "  olá mundo, bem vindo ao C#  ";
        Console.WriteLine($"Entrada: '{entrada}'");
        Console.WriteLine($"Saída:   '{processador.Processar(entrada)}'");
    }
}

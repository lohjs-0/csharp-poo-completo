// Exercício 07 — SOLUÇÃO
class Produto
{
    private double _preco;
    private int _estoque;

    public string Nome { get; set; }
    public string Categoria { get; set; }

    public double Preco
    {
        get => _preco;
        set { if (value < 0) throw new ArgumentException("Preço não pode ser negativo."); _preco = value; }
    }

    public int Estoque
    {
        get => _estoque;
        private set { if (value < 0) throw new ArgumentException("Estoque não pode ser negativo."); _estoque = value; }
    }

    public Produto(string nome, double preco, string categoria, int estoque = 0)
    {
        Nome = nome; Preco = preco; Categoria = categoria; Estoque = estoque;
    }

    public void AplicarDesconto(double percentual)
    {
        if (percentual <= 0 || percentual >= 100) throw new ArgumentException("Percentual inválido.");
        Preco *= (1 - percentual / 100);
    }

    public double PrecoComDesconto(double percentual) => Preco * (1 - percentual / 100);

    public void AdicionarEstoque(int qtd)
    {
        if (qtd <= 0) throw new ArgumentException("Quantidade deve ser positiva.");
        Estoque += qtd;
    }

    public bool RemoverEstoque(int qtd)
    {
        if (qtd > Estoque) { Console.WriteLine("Estoque insuficiente."); return false; }
        Estoque -= qtd;
        return true;
    }

    public bool EstaDisponivel() => Estoque > 0;

    public void ExibirInfo()
    {
        Console.WriteLine($"[{Categoria}] {Nome} — R${Preco:F2} | Estoque: {Estoque} | {(EstaDisponivel() ? "Disponível" : "Indisponível")}");
    }
}

class Programa
{
    static void Main()
    {
        var p = new Produto("Notebook", 3500.00, "Eletrônicos", 10);
        p.ExibirInfo();
        Console.WriteLine($"Com 15% desconto: R${p.PrecoComDesconto(15):F2}");
        p.AplicarDesconto(10);
        Console.WriteLine($"Após 10% desconto: R${p.Preco:F2}");
        p.RemoverEstoque(9);
        Console.WriteLine($"Disponível? {p.EstaDisponivel()}");
        p.RemoverEstoque(1);
        Console.WriteLine($"Disponível? {p.EstaDisponivel()}");
    }
}

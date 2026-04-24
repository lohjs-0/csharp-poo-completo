// Exercício 07 — Produto com Desconto
class Produto
{
    // TODO: Nome, Preco (validado > 0), Categoria, Estoque
    // TODO: AplicarDesconto(double percentual) — modifica o preço
    // TODO: PrecoComDesconto(double percentual) — retorna sem modificar
    // TODO: AdicionarEstoque / RemoverEstoque
    // TODO: EstaDisponivel() → bool
    // TODO: ExibirInfo()
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

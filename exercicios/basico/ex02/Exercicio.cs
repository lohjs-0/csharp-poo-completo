// Exercício 02 — Classe Retângulo
// Preencha os TODOs!

class Retangulo
{
    // TODO: Propriedades Largura, Altura, Cor
    // Lembre: Largura e Altura devem ser validadas no setter

    // TODO: Construtor(largura, altura)

    // TODO: Construtor(largura, altura, cor)

    // TODO: CalcularArea()

    // TODO: CalcularPerimetro()

    // TODO: EhQuadrado()

    // TODO: EscalarPor(double fator)

    // TODO: ExibirInfo()
}

class Programa
{
    static void Main()
    {
        var ret = new Retangulo(5, 3, "Azul");
        ret.ExibirInfo();
        Console.WriteLine($"É quadrado? {ret.EhQuadrado()}");

        Console.WriteLine("\nApós escalar por 2:");
        ret.EscalarPor(2);
        ret.ExibirInfo();

        // Teste com quadrado
        var quadrado = new Retangulo(4, 4);
        Console.WriteLine($"\nÉ quadrado? {quadrado.EhQuadrado()}"); // True

        // Teste de validação
        try
        {
            var invalido = new Retangulo(-1, 5);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"\nErro esperado: {ex.Message}");
        }
    }
}

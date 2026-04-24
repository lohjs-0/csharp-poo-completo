// Exercício 01 — Criar Classe Pessoa
// Preencha os TODOs abaixo!

class Pessoa
{
    // TODO: Declare as propriedades
    // Nome (string)
    // Idade (int)
    // Email (string)
    // Altura (double)

    // TODO: Crie um construtor que receba Nome e Idade

    // TODO: Crie um construtor que receba Nome, Idade e Email

    // TODO: Implemente o método Apresentar()
    // Saída esperada: "Olá! Meu nome é [Nome] e tenho [Idade] anos."
    public void Apresentar()
    {
        // seu código aqui
    }

    // TODO: Implemente o método EhMaiorDeIdade()
    // Retorna true se Idade >= 18
    public bool EhMaiorDeIdade()
    {
        // seu código aqui
        return false;
    }

    // TODO: Implemente o método IMC(double peso)
    // Fórmula: peso / (Altura * Altura)
    public double IMC(double peso)
    {
        // seu código aqui
        return 0;
    }

    // TODO: Implemente o método ClassificarIMC(double peso)
    // Use a tabela de classificação do README
    public string ClassificarIMC(double peso)
    {
        // seu código aqui
        return "";
    }
}

// Programa de teste — não modifique!
class Programa
{
    static void Main()
    {
        var pessoa = new Pessoa("Ana Silva", 25);
        pessoa.Altura = 1.65;
        pessoa.Email = "ana@email.com";

        pessoa.Apresentar();
        Console.WriteLine($"É maior de idade? {pessoa.EhMaiorDeIdade()}");

        double peso = 62;
        Console.WriteLine($"IMC: {pessoa.IMC(peso):F2}");
        Console.WriteLine($"Classificação: {pessoa.ClassificarIMC(peso)}");

        // Teste com menor de idade
        var jovem = new Pessoa("Lucas", 16);
        jovem.Apresentar();
        Console.WriteLine($"É maior de idade? {jovem.EhMaiorDeIdade()}");
    }
}

// Exercício 01 — SOLUÇÃO
// Tente resolver antes de ver a solução!

class Pessoa
{
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Email { get; set; }
    public double Altura { get; set; }

    public Pessoa(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
        Email = "";
    }

    public Pessoa(string nome, int idade, string email)
    {
        Nome = nome;
        Idade = idade;
        Email = email;
    }

    public void Apresentar()
    {
        Console.WriteLine($"Olá! Meu nome é {Nome} e tenho {Idade} anos.");
    }

    public bool EhMaiorDeIdade()
    {
        return Idade >= 18;
    }

    public double IMC(double peso)
    {
        if (Altura <= 0)
            throw new InvalidOperationException("Altura deve ser definida antes de calcular o IMC.");
        return Math.Round(peso / (Altura * Altura), 2);
    }

    public string ClassificarIMC(double peso)
    {
        double imc = IMC(peso);

        if (imc < 18.5) return "Abaixo do peso";
        if (imc < 25.0) return "Peso normal";
        if (imc < 30.0) return "Sobrepeso";
        return "Obesidade";
    }
}

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

        var jovem = new Pessoa("Lucas", 16);
        jovem.Apresentar();
        Console.WriteLine($"É maior de idade? {jovem.EhMaiorDeIdade()}");
    }
}

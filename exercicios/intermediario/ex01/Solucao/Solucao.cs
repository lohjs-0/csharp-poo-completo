// Intermediário 01 — SOLUÇÃO

abstract class Animal
{
    public string Nome { get; }
    public int Idade { get; }
    public double Peso { get; }

    protected Animal(string nome, int idade, double peso)
    {
        Nome = nome; Idade = idade; Peso = peso;
    }

    public virtual void FazerSom() => Console.WriteLine($"{Nome} faz algum som...");
    public void Comer() => Console.WriteLine($"{Nome} está comendo.");
    public void Dormir() => Console.WriteLine($"{Nome} está dormindo. zzz...");
    public override string ToString() => $"{GetType().Name} {Nome} ({Idade} anos, {Peso}kg)";
}

class Cachorro : Animal
{
    public string Raca { get; }

    public Cachorro(string nome, int idade, double peso, string raca) : base(nome, idade, peso) { Raca = raca; }

    public override void FazerSom() => Console.WriteLine("Au au!");
    public void Buscar() => Console.WriteLine($"{Nome} buscou o objeto!");
    public void Abanar() => Console.WriteLine($"{Nome} abanou o rabo!");
}

class Gato : Animal
{
    public bool EhCastrado { get; }

    public Gato(string nome, int idade, double peso, bool ehCastrado) : base(nome, idade, peso) { EhCastrado = ehCastrado; }

    public override void FazerSom() => Console.WriteLine("Miau!");
    public void Arranhar() => Console.WriteLine($"{Nome} arranhou algo!");
    public void Ronronar() => Console.WriteLine($"{Nome} ronrona: purrr...");
}

class Passaro : Animal
{
    public double Envergadura { get; }

    public Passaro(string nome, int idade, double peso, double envergadura) : base(nome, idade, peso) { Envergadura = envergadura; }

    public override void FazerSom() => Console.WriteLine("Piu piu!");
    public void Voar() => Console.WriteLine($"{Nome} está voando!");
    public void Pousar() => Console.WriteLine($"{Nome} pousou.");
}

class Programa
{
    static void Main()
    {
        var animais = new List<Animal>
        {
            new Cachorro("Rex", 3, 25, "Labrador"),
            new Gato("Mingau", 5, 4, true),
            new Passaro("Piu", 2, 0.5, 30),
            new Cachorro("Bolinha", 1, 10, "Poodle")
        };

        Console.WriteLine("=== Todos os sons ===");
        foreach (var animal in animais) { Console.Write($"{animal.Nome}: "); animal.FazerSom(); }

        Console.WriteLine("\n=== Apenas cachorros ===");
        foreach (var animal in animais)
            if (animal is Cachorro c) c.Buscar();
    }
}

// Intermediário 01 — Herança: Animal
// Implemente a hierarquia conforme o README

abstract class Animal
{
    // TODO: Nome, Idade, Peso
    // TODO: Construtor(nome, idade, peso)
    // TODO: virtual FazerSom()
    // TODO: Comer(), Dormir()
    // TODO: override ToString()
}

class Cachorro : Animal
{
    // TODO: Raca
    // TODO: override FazerSom() — "Au au!"
    // TODO: Buscar(), Abanar()
}

class Gato : Animal
{
    // TODO: EhCastrado
    // TODO: override FazerSom() — "Miau!"
    // TODO: Arranhar(), Ronronar()
}

class Passaro : Animal
{
    // TODO: Envergadura
    // TODO: override FazerSom() — "Piu piu!"
    // TODO: Voar(), Pousar()
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
        foreach (var animal in animais)
        {
            Console.Write($"{animal.Nome}: ");
            animal.FazerSom();
        }

        Console.WriteLine("\n=== Apenas cachorros ===");
        foreach (var animal in animais)
            if (animal is Cachorro cachorro)
                cachorro.Buscar();
    }
}

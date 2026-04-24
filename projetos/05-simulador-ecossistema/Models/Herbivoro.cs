namespace SimuladorEcossistema.Models;

public class Herbivoro : Animal
{
    public Herbivoro(string nome) : base(nome, 60, 8) { }

    public override void Alimentar()
    {
        Console.WriteLine($"  🐇 {Nome} come plantas.");
        Fome = Math.Max(0, Fome - 30);
        Saude = Math.Min(100, Saude + 10);
    }

    public override SerVivo? Reproduzir()
    {
        if (Saude >= 50 && new Random().Next(100) < 40)
            return new Herbivoro($"Coelho-{Guid.NewGuid().ToString()[..4]}");
        return null;
    }
}
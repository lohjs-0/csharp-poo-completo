namespace SimuladorEcossistema.Models;

public class Carnivoro : Animal, IPredador
{
    public int ForcaAtaque { get; private set; }

    public Carnivoro(string nome) : base(nome, 80, 6)
    {
        ForcaAtaque = 25;
    }

    public override void Alimentar()
    {
        Console.WriteLine($"  🐺 {Nome} procura por presas...");
        Fome = Math.Max(0, Fome - 5);
    }

    public void Caçar(IPresa presa)
    {
        Console.WriteLine($"  🐺 {Nome} ataca {presa.Nome}!");
        presa.SerAtacado(ForcaAtaque);
        if (!presa.EstaVivo)
        {
            Console.WriteLine($"  ☠️  {presa.Nome} morreu! {Nome} se alimentou.");
            Fome = Math.Max(0, Fome - 50);
            Saude = Math.Min(100, Saude + 20);
        }
    }

    public override SerVivo? Reproduzir()
    {
        if (Saude >= 60 && new Random().Next(100) < 20)
            return new Carnivoro($"Lobo-{Guid.NewGuid().ToString()[..4]}");
        return null;
    }
}
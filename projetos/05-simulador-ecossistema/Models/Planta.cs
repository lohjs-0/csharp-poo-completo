namespace SimuladorEcossistema.Models;

public class Planta : SerVivo
{
    public Planta(string nome) : base(nome, 50) { }

    public override void Alimentar()
    {
        // Plantas se alimentam de luz solar — regeneram saúde
        Saude = Math.Min(100, Saude + 10);
    }

    public override SerVivo? Reproduzir()
    {
        if (Saude >= 40 && new Random().Next(100) < 30)
            return new Planta($"Planta-{Guid.NewGuid().ToString()[..4]}");
        return null;
    }

    public void ReceberDano(int dano)
    {
        Saude -= dano;
        if (Saude < 0) Saude = 0;
    }
}
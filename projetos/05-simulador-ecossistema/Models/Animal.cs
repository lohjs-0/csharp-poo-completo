namespace SimuladorEcossistema.Models;

public abstract class Animal : SerVivo, IPresa
{
    public int Fome { get; protected set; }
    public int Velocidade { get; protected set; }

    protected Animal(string nome, int saude, int velocidade) : base(nome, saude)
    {
        Fome = 0;
        Velocidade = velocidade;
    }

    public override void Envelhecer()
    {
        base.Envelhecer();
        Fome += 10;
        if (Fome >= 100)
        {
            Saude -= 15;
            if (Saude < 0) Saude = 0;
        }
    }

    public void SerAtacado(int dano)
    {
        Saude -= dano;
        if (Saude < 0) Saude = 0;
        Console.WriteLine($"  💥 {Nome} recebeu {dano} de dano! Saúde restante: {Saude}");
    }
}
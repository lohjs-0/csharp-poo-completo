namespace SimuladorEcossistema.Models;

public abstract class SerVivo
{
    public string Nome { get; protected set; }
    public int Saude { get; protected set; }
    public int Idade { get; protected set; }
    public bool EstaVivo => Saude > 0;

    protected SerVivo(string nome, int saudeInicial)
    {
        Nome = nome;
        Saude = saudeInicial;
        Idade = 0;
    }

    public abstract void Alimentar();
    public abstract SerVivo? Reproduzir();

    public virtual void Envelhecer()
    {
        Idade++;
        Saude -= 2;
        if (Saude < 0) Saude = 0;
    }

    public override string ToString() => $"{Nome} (Saúde: {Saude}, Idade: {Idade})";
}
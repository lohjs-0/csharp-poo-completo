namespace SimuladorTrafego.Models;

public abstract class Veiculo
{
    public string Id { get; }
    public int Velocidade { get; protected set; }
    public int VelocidadeMaxima { get; }
    public string RuaAtual { get; set; } = "";

    protected Veiculo(string id, int velocidadeMaxima)
    {
        Id = id;
        VelocidadeMaxima = velocidadeMaxima;
        Velocidade = velocidadeMaxima;
    }

    public abstract void Acelerar();
    public abstract void Frear();
    public abstract string Icone { get; }

    public override string ToString() => $"{Icone} {Id} ({Velocidade}km/h)";
}

public class Carro : Veiculo
{
    public Carro(string id) : base(id, 60) { }
    public override string Icone => "🚗";
    public override void Acelerar() => Velocidade = Math.Min(VelocidadeMaxima, Velocidade + 10);
    public override void Frear() => Velocidade = Math.Max(0, Velocidade - 20);
}

public class Moto : Veiculo
{
    public Moto(string id) : base(id, 90) { }
    public override string Icone => "🏍️";
    public override void Acelerar() => Velocidade = Math.Min(VelocidadeMaxima, Velocidade + 15);
    public override void Frear() => Velocidade = Math.Max(0, Velocidade - 25);
}
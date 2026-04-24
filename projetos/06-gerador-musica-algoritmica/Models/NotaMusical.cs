namespace GeradorMusica.Models;

public class NotaMusical
{
    public string Nome { get; }
    public double Frequencia { get; }
    public double Duracao { get; }
    public int Volume { get; }

    public NotaMusical(string nome, double frequencia, double duracao = 0.5, int volume = 80)
    {
        Nome = nome;
        Frequencia = frequencia;
        Duracao = duracao;
        Volume = Math.Clamp(volume, 0, 127);
    }

    public override string ToString() => $"{Nome}({Duracao}s)";
}
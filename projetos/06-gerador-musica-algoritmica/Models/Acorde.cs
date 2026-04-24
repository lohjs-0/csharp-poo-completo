namespace GeradorMusica.Models;

public class Acorde
{
    public string Nome { get; }
    public List<NotaMusical> Notas { get; } = new();

    public Acorde(string nome)
    {
        Nome = nome;
    }

    public void AdicionarNota(NotaMusical nota) => Notas.Add(nota);

    public override string ToString() => $"[{Nome}: {string.Join(", ", Notas)}]";
}
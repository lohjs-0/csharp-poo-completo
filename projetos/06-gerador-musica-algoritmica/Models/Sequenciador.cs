namespace GeradorMusica.Models;

public class Sequenciador
{
    private readonly List<NotaMusical> _notas = new();
    public IReadOnlyList<NotaMusical> Notas => _notas.AsReadOnly();

    public void AdicionarNota(NotaMusical nota) => _notas.Add(nota);

    public void Limpar() => _notas.Clear();

    public void ExibirPartitura()
    {
        Console.WriteLine("\n📄 Partitura:");
        Console.Write("  | ");
        foreach (var nota in _notas)
            Console.Write($"{nota,-12}| ");
        Console.WriteLine();
    }

    public string Exportar() =>
        string.Join(" > ", _notas.Select(n => n.ToString()));
}
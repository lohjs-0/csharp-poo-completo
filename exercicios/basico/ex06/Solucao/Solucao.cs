// Exercício 06 — SOLUÇÃO
class Aluno
{
    private static int _proximaMatricula = 2024001;
    private readonly List<double> _notas = new();

    public string Nome { get; }
    public int Matricula { get; }

    public Aluno(string nome)
    {
        Nome = nome;
        Matricula = _proximaMatricula++;
    }

    public void AdicionarNota(double nota)
    {
        if (nota < 0 || nota > 10) throw new ArgumentOutOfRangeException("Nota deve ser entre 0 e 10.");
        _notas.Add(nota);
    }

    public double CalcularMedia() => _notas.Count == 0 ? 0 : _notas.Average();
    public double ObterMaiorNota() => _notas.Count == 0 ? 0 : _notas.Max();
    public double ObterMenorNota() => _notas.Count == 0 ? 0 : _notas.Min();

    public string ObterSituacao()
    {
        double m = CalcularMedia();
        return m >= 7 ? "Aprovado" : m >= 5 ? "Recuperação" : "Reprovado";
    }

    public void ExibirBoletim()
    {
        Console.WriteLine($"\n=== Boletim: {Nome} (Mat: {Matricula}) ===");
        for (int i = 0; i < _notas.Count; i++)
            Console.WriteLine($"  Nota {i+1}: {_notas[i]:F1}");
        Console.WriteLine($"  Média: {CalcularMedia():F2} | Maior: {ObterMaiorNota():F1} | Menor: {ObterMenorNota():F1}");
        Console.WriteLine($"  Situação: {ObterSituacao()}");
    }
}

class Programa
{
    static void Main()
    {
        var a1 = new Aluno("Ana");
        a1.AdicionarNota(8.5); a1.AdicionarNota(7.0); a1.AdicionarNota(9.5);
        a1.ExibirBoletim();
        var a2 = new Aluno("Pedro");
        a2.AdicionarNota(4.0); a2.AdicionarNota(6.0);
        a2.ExibirBoletim();
    }
}

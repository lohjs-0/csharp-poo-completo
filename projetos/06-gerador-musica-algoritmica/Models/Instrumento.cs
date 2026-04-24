namespace GeradorMusica.Models;

public abstract class Instrumento
{
    public string Nome { get; }

    protected Instrumento(string nome)
    {
        Nome = nome;
    }

    public abstract string TocarNota(NotaMusical nota);

    public virtual void TocarSequencia(IEnumerable<NotaMusical> notas)
    {
        Console.WriteLine($"\n🎵 {Nome} tocando:");
        foreach (var nota in notas)
            Console.Write($"  {TocarNota(nota)}");
        Console.WriteLine();
    }
}

public class Piano : Instrumento
{
    public Piano() : base("Piano") { }

    public override string TocarNota(NotaMusical nota) =>
        $"🎹 ♩{nota.Nome} ";
}

public class Bateria : Instrumento
{
    public Bateria() : base("Bateria") { }

    public override string TocarNota(NotaMusical nota) =>
        $"🥁 *{nota.Nome}* ";
}

public class Flauta : Instrumento
{
    public Flauta() : base("Flauta") { }

    public override string TocarNota(NotaMusical nota) =>
        $"🪈 ~{nota.Nome}~ ";
}
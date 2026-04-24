namespace Biblioteca.Models;

public class Emprestimo
{
    private static int _proximoId = 1;

    public int Id { get; }
    public Livro Livro { get; }
    public Membro Membro { get; }
    public DateTime DataEmprestimo { get; }
    public DateTime DataPrevistaDevolucao { get; }
    public DateTime? DataDevolucao { get; private set; }
    public bool Devolvido => DataDevolucao.HasValue;

    private const double MultaPorDia = 1.50;
    private const int PrazoPadraoDias = 14;

    public Emprestimo(Livro livro, Membro membro, int prazoEmDias = PrazoPadraoDias)
    {
        Id = _proximoId++;
        Livro = livro;
        Membro = membro;
        DataEmprestimo = DateTime.Now;
        DataPrevistaDevolucao = DataEmprestimo.AddDays(prazoEmDias);
    }

    public bool EstaAtrasado() => !Devolvido && DateTime.Now > DataPrevistaDevolucao;

    public int DiasAtraso()
    {
        if (!EstaAtrasado()) return 0;
        var referencia = DataDevolucao ?? DateTime.Now;
        return (int)(referencia - DataPrevistaDevolucao).TotalDays;
    }

    public double CalcularMulta() => DiasAtraso() * MultaPorDia;

    public double Devolver()
    {
        if (Devolvido) throw new InvalidOperationException("Livro já devolvido.");
        DataDevolucao = DateTime.Now;
        Livro.Disponivel = true;
        double multa = CalcularMulta();
        return multa;
    }

    public void ExibirDetalhes()
    {
        Console.WriteLine($"Empréstimo #{Id:D4}");
        Console.WriteLine($"  Livro:    {Livro.Titulo}");
        Console.WriteLine($"  Membro:   {Membro.Nome}");
        Console.WriteLine($"  Retirada: {DataEmprestimo:dd/MM/yyyy}");
        Console.WriteLine($"  Prazo:    {DataPrevistaDevolucao:dd/MM/yyyy}");
        if (Devolvido)
            Console.WriteLine($"  Devolvido:{DataDevolucao:dd/MM/yyyy} {(CalcularMulta() > 0 ? $"| Multa: R${CalcularMulta():F2}" : "")}");
        else if (EstaAtrasado())
            Console.WriteLine($"  Status:   ⚠️ ATRASADO {DiasAtraso()} dias (multa: R${CalcularMulta():F2})");
        else
            Console.WriteLine($"  Status:   Em aberto");
    }
}

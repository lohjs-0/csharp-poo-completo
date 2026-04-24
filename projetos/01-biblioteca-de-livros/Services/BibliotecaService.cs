using Biblioteca.Models;

namespace Biblioteca.Services;

public class BibliotecaService
{
    private readonly List<Livro> _livros = new();
    private readonly List<Membro> _membros = new();
    private readonly List<Emprestimo> _emprestimos = new();

    public string Nome { get; }

    public BibliotecaService(string nome) { Nome = nome; }

    // ==================== LIVROS ====================

    public Livro AdicionarLivro(string titulo, string autor, string isbn,
        int ano, string genero, int paginas = 200)
    {
        var livro = new Livro(titulo, autor, isbn, ano, genero, paginas);
        _livros.Add(livro);
        Console.WriteLine($"✓ Livro adicionado: {livro.Titulo}");
        return livro;
    }

    public List<Livro> BuscarLivros(string termo) =>
        _livros.Where(l =>
            l.Titulo.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
            l.Autor.Contains(termo, StringComparison.OrdinalIgnoreCase) ||
            l.Genero.Contains(termo, StringComparison.OrdinalIgnoreCase))
        .ToList();

    public List<Livro> LivrosDisponiveis() => _livros.Where(l => l.Disponivel).ToList();

    public List<Livro> LivrosMaisEmprestados(int top = 5) =>
        _livros.OrderByDescending(l => l.TotalEmprestimos).Take(top).ToList();

    // ==================== MEMBROS ====================

    public Membro CadastrarMembro(string nome, string email, string cpf)
    {
        if (_membros.Any(m => m.Email == email.ToLower()))
            throw new InvalidOperationException($"E-mail '{email}' já cadastrado.");

        var membro = new Membro(nome, email, cpf);
        _membros.Add(membro);
        Console.WriteLine($"✓ Membro cadastrado: {membro.Nome}");
        return membro;
    }

    public Membro? BuscarMembro(string nomeOuEmail) =>
        _membros.FirstOrDefault(m =>
            m.Nome.Contains(nomeOuEmail, StringComparison.OrdinalIgnoreCase) ||
            m.Email.Equals(nomeOuEmail, StringComparison.OrdinalIgnoreCase));

    // ==================== EMPRÉSTIMOS ====================

    public Emprestimo RealizarEmprestimo(int livroId, int membroId, int prazo = 14)
    {
        var livro = _livros.FirstOrDefault(l => l.Id == livroId)
            ?? throw new ArgumentException($"Livro #{livroId} não encontrado.");

        var membro = _membros.FirstOrDefault(m => m.Id == membroId)
            ?? throw new ArgumentException($"Membro #{membroId} não encontrado.");

        if (!livro.Disponivel)
            throw new InvalidOperationException($"Livro '{livro.Titulo}' não está disponível.");

        if (membro.TemPendencias())
            throw new InvalidOperationException($"Membro {membro.Nome} possui empréstimos atrasados.");

        if (membro.EmprestimosAtivos() >= 3)
            throw new InvalidOperationException("Limite de 3 empréstimos simultâneos atingido.");

        livro.Disponivel = false;
        livro.TotalEmprestimos++;

        var emprestimo = new Emprestimo(livro, membro, prazo);
        _emprestimos.Add(emprestimo);
        membro.Emprestimos.Add(emprestimo);

        Console.WriteLine($"✓ Empréstimo #{emprestimo.Id:D4}: '{livro.Titulo}' → {membro.Nome}");
        Console.WriteLine($"  Prazo: {emprestimo.DataPrevistaDevolucao:dd/MM/yyyy}");
        return emprestimo;
    }

    public double DevolverLivro(int emprestimoId)
    {
        var emprestimo = _emprestimos.FirstOrDefault(e => e.Id == emprestimoId)
            ?? throw new ArgumentException($"Empréstimo #{emprestimoId} não encontrado.");

        double multa = emprestimo.Devolver();
        Console.WriteLine($"✓ Livro '{emprestimo.Livro.Titulo}' devolvido por {emprestimo.Membro.Nome}");
        if (multa > 0)
            Console.WriteLine($"  ⚠️ Multa por atraso: R${multa:F2}");
        return multa;
    }

    // ==================== RELATÓRIOS ====================

    public void ExibirRelatorio()
    {
        Console.WriteLine($"\n{'═',50}");
        Console.WriteLine($"  RELATÓRIO — {Nome}");
        Console.WriteLine($"{'═',50}");
        Console.WriteLine($"  Total de livros:   {_livros.Count}");
        Console.WriteLine($"  Livros disponíveis:{LivrosDisponiveis().Count}");
        Console.WriteLine($"  Total de membros:  {_membros.Count}");
        Console.WriteLine($"  Empréstimos ativos:{_emprestimos.Count(e => !e.Devolvido)}");
        Console.WriteLine($"  Empréstimos atras.:{_emprestimos.Count(e => e.EstaAtrasado())}");

        Console.WriteLine("\n  📊 Top 3 mais emprestados:");
        foreach (var l in LivrosMaisEmprestados(3))
            Console.WriteLine($"    {l.TotalEmprestimos}x — {l.Titulo}");
        Console.WriteLine();
    }
}

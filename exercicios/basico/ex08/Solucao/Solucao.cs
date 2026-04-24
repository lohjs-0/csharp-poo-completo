// Exercício 08 — SOLUÇÃO

class Livro
{
    public string Titulo { get; }
    public string Autor { get; }
    public string ISBN { get; }
    public int AnoPublicacao { get; }
    public int NumeroPaginas { get; }
    public string Genero { get; }

    public Livro(string titulo, string autor, string isbn, int ano, int paginas, string genero)
    {
        Titulo = titulo; Autor = autor; ISBN = isbn;
        AnoPublicacao = ano; NumeroPaginas = paginas; Genero = genero;
    }

    public int IdadeDoLivro() => DateTime.Now.Year - AnoPublicacao;
    public bool EhAntigo() => IdadeDoLivro() > 50;

    public void ExibirFicha()
    {
        Console.WriteLine($"📚 {Titulo}");
        Console.WriteLine($"   Autor: {Autor} | Gênero: {Genero}");
        Console.WriteLine($"   Ano: {AnoPublicacao} ({IdadeDoLivro()} anos) | Páginas: {NumeroPaginas}");
        if (EhAntigo()) Console.WriteLine("   ⭐ Obra clássica");
    }
}

class Biblioteca
{
    public string Nome { get; }
    private readonly List<Livro> _livros = new();

    public Biblioteca(string nome) { Nome = nome; }

    public void AdicionarLivro(Livro livro) { _livros.Add(livro); Console.WriteLine($"'{livro.Titulo}' adicionado."); }

    public List<Livro> BuscarPorAutor(string autor) =>
        _livros.Where(l => l.Autor.Contains(autor, StringComparison.OrdinalIgnoreCase)).ToList();

    public List<Livro> BuscarPorGenero(string genero) =>
        _livros.Where(l => l.Genero.Equals(genero, StringComparison.OrdinalIgnoreCase)).ToList();

    public Livro? LivroMaisAntigo() => _livros.OrderBy(l => l.AnoPublicacao).FirstOrDefault();

    public void ExibirCatalogo()
    {
        Console.WriteLine($"\n=== {Nome} ({_livros.Count} livros) ===");
        foreach (var livro in _livros.OrderBy(l => l.Titulo)) livro.ExibirFicha();
    }
}

class Programa
{
    static void Main()
    {
        var bib = new Biblioteca("Biblioteca Municipal");
        bib.AdicionarLivro(new Livro("Dom Casmurro", "Machado de Assis", "978-01", 1899, 256, "Romance"));
        bib.AdicionarLivro(new Livro("O Alquimista", "Paulo Coelho", "978-02", 1988, 208, "Ficção"));
        bib.AdicionarLivro(new Livro("Clean Code", "Robert Martin", "978-03", 2008, 431, "Tecnologia"));
        bib.ExibirCatalogo();
        Console.WriteLine($"\nLivro mais antigo: {bib.LivroMaisAntigo()?.Titulo}");
    }
}

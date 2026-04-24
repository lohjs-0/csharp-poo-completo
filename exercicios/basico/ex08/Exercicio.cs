// Exercício 08 — Classe Livro + Biblioteca

class Livro
{
    // TODO: Titulo, Autor, ISBN, AnoPublicacao, NumeroPaginas, Genero
    // TODO: EhAntigo() — mais de 50 anos
    // TODO: IdadeDoLivro() — anos desde publicação
    // TODO: ExibirFicha()
}

class Biblioteca
{
    // TODO: Nome da biblioteca
    // TODO: Lista de livros
    // TODO: AdicionarLivro(Livro)
    // TODO: BuscarPorAutor(string autor) → List<Livro>
    // TODO: BuscarPorGenero(string genero) → List<Livro>
    // TODO: ExibirCatalogo()
    // TODO: LivroMaisAntigo() → Livro
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
        Console.WriteLine($"\nLivro mais antigo: {bib.LivroMaisAntigo().Titulo}");
    }
}

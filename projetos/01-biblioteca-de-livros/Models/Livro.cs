namespace Biblioteca.Models;

public class Livro
{
    private static int _proximoId = 1;

    public int Id { get; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string ISBN { get; }
    public int AnoPublicacao { get; set; }
    public string Genero { get; set; }
    public int NumeroPaginas { get; set; }
    public bool Disponivel { get; set; } = true;
    public int TotalEmprestimos { get; set; } = 0;

    public Livro(string titulo, string autor, string isbn, int anoPublicacao, string genero, int paginas = 200)
    {
        if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("Título obrigatório.");
        if (string.IsNullOrWhiteSpace(autor)) throw new ArgumentException("Autor obrigatório.");

        Id = _proximoId++;
        Titulo = titulo.Trim();
        Autor = autor.Trim();
        ISBN = isbn;
        AnoPublicacao = anoPublicacao;
        Genero = genero;
        NumeroPaginas = paginas;
    }

    public int IdadeDoLivro() => DateTime.Now.Year - AnoPublicacao;

    public override string ToString() =>
        $"[{Id:D3}] '{Titulo}' por {Autor} ({AnoPublicacao}) — {(Disponivel ? "✓ Disponível" : "✗ Emprestado")}";
}

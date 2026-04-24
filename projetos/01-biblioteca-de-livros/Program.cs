using Biblioteca.Models;
using Biblioteca.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("╔══════════════════════════════════╗");
Console.WriteLine("║   Sistema de Biblioteca Digital  ║");
Console.WriteLine("╚══════════════════════════════════╝\n");

var biblioteca = new BibliotecaService("Biblioteca Municipal de São Paulo");

// ── Cadastrar livros ──
Console.WriteLine("=== Cadastrando Livros ===");
var domCasmurro = biblioteca.AdicionarLivro("Dom Casmurro", "Machado de Assis", "978-85-01", 1899, "Romance", 256);
var alquimista = biblioteca.AdicionarLivro("O Alquimista", "Paulo Coelho", "978-85-02", 1988, "Ficção", 208);
var cleanCode = biblioteca.AdicionarLivro("Clean Code", "Robert C. Martin", "978-01-03", 2008, "Tecnologia", 431);
var sapiens = biblioteca.AdicionarLivro("Sapiens", "Yuval Noah Harari", "978-85-04", 2011, "História", 443);
var hobbit = biblioteca.AdicionarLivro("O Hobbit", "J.R.R. Tolkien", "978-85-05", 1937, "Fantasia", 310);
var cleanArch = biblioteca.AdicionarLivro("Clean Architecture", "Robert C. Martin", "978-01-06", 2017, "Tecnologia", 432);

// ── Cadastrar membros ──
Console.WriteLine("\n=== Cadastrando Membros ===");
var ana = biblioteca.CadastrarMembro("Ana Silva", "ana@email.com", "12345678901");
var carlos = biblioteca.CadastrarMembro("Carlos Lima", "carlos@email.com", "98765432100");
var maria = biblioteca.CadastrarMembro("Maria Santos", "maria@email.com", "11122233344");

// ── Empréstimos ──
Console.WriteLine("\n=== Realizando Empréstimos ===");
var emp1 = biblioteca.RealizarEmprestimo(domCasmurro.Id, ana.Id);
var emp2 = biblioteca.RealizarEmprestimo(cleanCode.Id, ana.Id);
var emp3 = biblioteca.RealizarEmprestimo(alquimista.Id, carlos.Id);
var emp4 = biblioteca.RealizarEmprestimo(sapiens.Id, maria.Id);
var emp5 = biblioteca.RealizarEmprestimo(cleanArch.Id, carlos.Id);

// ── Tentar emprestar indisponível ──
Console.WriteLine("\n=== Tentando emprestar livro indisponível ===");
try { biblioteca.RealizarEmprestimo(cleanCode.Id, maria.Id); }
catch (InvalidOperationException ex) { Console.WriteLine($"✗ {ex.Message}"); }

// ── Devolução ──
Console.WriteLine("\n=== Devoluções ===");
biblioteca.DevolverLivro(emp1.Id);
biblioteca.DevolverLivro(emp3.Id);

// ── Busca ──
Console.WriteLine("\n=== Busca por 'Robert' ===");
foreach (var livro in biblioteca.BuscarLivros("Robert"))
    Console.WriteLine($"  {livro}");

// ── Perfil de membro ──
Console.WriteLine("\n=== Perfil de Ana ===");
ana.ExibirPerfil();

// ── Detalhes dos empréstimos ──
Console.WriteLine("\n=== Histórico de Empréstimos de Ana ===");
foreach (var e in ana.Emprestimos) e.ExibirDetalhes();

// ── Relatório ──
biblioteca.ExibirRelatorio();

Console.WriteLine("Pressione qualquer tecla para sair...");
Console.ReadKey();

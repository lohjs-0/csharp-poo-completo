namespace Biblioteca.Models;

public class Membro
{
    private static int _proximoId = 1;

    public int Id { get; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string CPF { get; }
    public DateTime DataCadastro { get; }
    public bool Ativo { get; set; } = true;
    public List<Emprestimo> Emprestimos { get; } = new();

    public Membro(string nome, string email, string cpf)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome obrigatório.");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email obrigatório.");

        Id = _proximoId++;
        Nome = nome.Trim();
        Email = email.Trim().ToLower();
        CPF = new string(cpf.Where(char.IsDigit).ToArray());
        DataCadastro = DateTime.Now;
    }

    public int EmprestimosAtivos() => Emprestimos.Count(e => !e.Devolvido);
    public bool TemPendencias() => Emprestimos.Any(e => !e.Devolvido && e.EstaAtrasado());

    public void ExibirPerfil()
    {
        Console.WriteLine($"Membro #{Id}: {Nome}");
        Console.WriteLine($"  Email: {Email}");
        Console.WriteLine($"  CPF: {CPF[..3]}.{CPF[3..6]}.{CPF[6..9]}-{CPF[9..]}");
        Console.WriteLine($"  Cadastro: {DataCadastro:dd/MM/yyyy}");
        Console.WriteLine($"  Empréstimos ativos: {EmprestimosAtivos()}");
        Console.WriteLine($"  Tem pendências: {(TemPendencias() ? "⚠️ SIM" : "Não")}");
    }
}

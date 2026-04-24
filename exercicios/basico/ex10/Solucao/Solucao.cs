// Exercício 10 — SOLUÇÃO
class Contato
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Grupo { get; set; }

    public Contato(string nome, string telefone, string email, string grupo = "Geral")
    {
        Nome = nome; Telefone = telefone; Email = email; Grupo = grupo;
    }

    public override string ToString() => $"{Nome} | {Telefone} | {Email} [{Grupo}]";
}

class AgendaContatos
{
    private readonly List<Contato> _contatos = new();
    public int Count => _contatos.Count;

    public void AdicionarContato(Contato contato) { _contatos.Add(contato); Console.WriteLine($"Contato '{contato.Nome}' adicionado."); }

    public bool RemoverContato(string nome)
    {
        var c = _contatos.FirstOrDefault(x => x.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        if (c == null) return false;
        _contatos.Remove(c); return true;
    }

    public List<Contato> BuscarPorNome(string termo) =>
        _contatos.Where(c => c.Nome.Contains(termo, StringComparison.OrdinalIgnoreCase)).ToList();

    public List<Contato> BuscarPorGrupo(string grupo) =>
        _contatos.Where(c => c.Grupo.Equals(grupo, StringComparison.OrdinalIgnoreCase)).ToList();

    public List<Contato> ListarOrdenados() => _contatos.OrderBy(c => c.Nome).ToList();

    public void ExibirAgenda()
    {
        Console.WriteLine($"\n=== Agenda ({Count} contatos) ===");
        foreach (var c in ListarOrdenados()) Console.WriteLine($"  {c}");
    }
}

class Programa
{
    static void Main()
    {
        var agenda = new AgendaContatos();
        agenda.AdicionarContato(new Contato("Ana Silva", "11 9999-0001", "ana@email.com", "Família"));
        agenda.AdicionarContato(new Contato("Bruno Lima", "11 9999-0002", "bruno@email.com", "Trabalho"));
        agenda.AdicionarContato(new Contato("Carlos Santos", "11 9999-0003", "carlos@email.com", "Amigos"));
        agenda.AdicionarContato(new Contato("Amanda Costa", "11 9999-0004", "amanda@email.com", "Trabalho"));
        agenda.ExibirAgenda();
        Console.WriteLine("\nBusca por 'a':");
        foreach (var c in agenda.BuscarPorNome("a")) Console.WriteLine($"  {c}");
        Console.WriteLine("\nGrupo Trabalho:");
        foreach (var c in agenda.BuscarPorGrupo("Trabalho")) Console.WriteLine($"  {c}");
    }
}

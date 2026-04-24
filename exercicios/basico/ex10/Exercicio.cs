// Exercício 10 — Lista de Contatos
class Contato
{
    // TODO: Nome, Telefone, Email, Grupo
    // TODO: Construtor e ToString()
}

class AgendaContatos
{
    // TODO: Lista de contatos
    // TODO: AdicionarContato, RemoverContato
    // TODO: BuscarPorNome (busca parcial)
    // TODO: BuscarPorGrupo
    // TODO: ListarOrdenados
    // TODO: ExibirAgenda
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

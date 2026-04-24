namespace Tarefas.Models;

public enum Prioridade { Baixa = 1, Media = 2, Alta = 3, Critica = 4 }
public enum StatusTarefa { Pendente, EmProgresso, Concluida, Cancelada }

public class Tarefa : IComparable<Tarefa>
{
    private static int _proximoId = 1;

    public int Id { get; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public Prioridade Prioridade { get; set; }
    public StatusTarefa Status { get; private set; } = StatusTarefa.Pendente;
    public string Categoria { get; set; }
    public string Responsavel { get; set; }
    public DateTime DataCriacao { get; }
    public DateTime? Prazo { get; set; }
    public DateTime? DataConclusao { get; private set; }
    public List<string> Tags { get; } = new();

    // Eventos
    public event Action<Tarefa>? StatusAlterado;
    public event Action<Tarefa>? PrazoAtrasado;

    public Tarefa(string titulo, string descricao, Prioridade prioridade,
        string categoria, string responsavel, DateTime? prazo = null)
    {
        if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("Título obrigatório.");
        Id = _proximoId++;
        Titulo = titulo.Trim();
        Descricao = descricao;
        Prioridade = prioridade;
        Categoria = categoria;
        Responsavel = responsavel;
        Prazo = prazo;
        DataCriacao = DateTime.Now;
    }

    public bool EstaAtrasada => Prazo.HasValue && DateTime.Now > Prazo && Status != StatusTarefa.Concluida;
    public int DiasAtraso => EstaAtrasada ? (int)(DateTime.Now - Prazo!.Value).TotalDays : 0;
    public bool Ativa => Status is StatusTarefa.Pendente or StatusTarefa.EmProgresso;

    public void IniciarProgresso()
    {
        if (Status != StatusTarefa.Pendente)
            throw new InvalidOperationException("Só pode iniciar tarefas pendentes.");
        Status = StatusTarefa.EmProgresso;
        StatusAlterado?.Invoke(this);
        if (EstaAtrasada) PrazoAtrasado?.Invoke(this);
    }

    public void Concluir()
    {
        if (Status == StatusTarefa.Cancelada)
            throw new InvalidOperationException("Não pode concluir tarefa cancelada.");
        Status = StatusTarefa.Concluida;
        DataConclusao = DateTime.Now;
        StatusAlterado?.Invoke(this);
    }

    public void Cancelar()
    {
        if (Status == StatusTarefa.Concluida)
            throw new InvalidOperationException("Não pode cancelar tarefa já concluída.");
        Status = StatusTarefa.Cancelada;
        StatusAlterado?.Invoke(this);
    }

    public void AdicionarTag(string tag)
    {
        if (!Tags.Contains(tag, StringComparer.OrdinalIgnoreCase))
            Tags.Add(tag);
    }

    // IComparable — ordena por prioridade decrescente, depois por prazo
    public int CompareTo(Tarefa? other)
    {
        if (other == null) return 1;
        int cmp = other.Prioridade.CompareTo(Prioridade); // maior prioridade primeiro
        if (cmp != 0) return cmp;
        if (Prazo.HasValue && other.Prazo.HasValue) return Prazo.Value.CompareTo(other.Prazo.Value);
        return 0;
    }

    public string PrioridadeEmoji() => Prioridade switch
    {
        Prioridade.Critica => "🔴",
        Prioridade.Alta => "🟠",
        Prioridade.Media => "🟡",
        Prioridade.Baixa => "🟢",
        _ => "⚪"
    };

    public string StatusEmoji() => Status switch
    {
        StatusTarefa.Pendente => "⏳",
        StatusTarefa.EmProgresso => "🔄",
        StatusTarefa.Concluida => "✅",
        StatusTarefa.Cancelada => "❌",
        _ => "?"
    };

    public void ExibirDetalhes()
    {
        string prazoStr = Prazo.HasValue
            ? $"{Prazo:dd/MM/yyyy}{(EstaAtrasada ? $" ⚠️ {DiasAtraso}d atrasada" : "")}"
            : "Sem prazo";

        Console.WriteLine($"  #{Id:D4} {PrioridadeEmoji()} {StatusEmoji()} {Titulo}");
        Console.WriteLine($"       Categoria: {Categoria} | Resp: {Responsavel}");
        Console.WriteLine($"       Prazo: {prazoStr}");
        if (Tags.Count > 0) Console.WriteLine($"       Tags: {string.Join(", ", Tags)}");
        if (!string.IsNullOrEmpty(Descricao)) Console.WriteLine($"       {Descricao}");
    }
}

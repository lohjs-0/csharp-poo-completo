using Tarefas.Models;

namespace Tarefas.Services;

public class GerenciadorTarefas
{
    private readonly List<Tarefa> _tarefas = new();

    // Delegates para filtro e ordenação
    public delegate bool FiltroDeTarefa(Tarefa t);
    public delegate IOrderedEnumerable<Tarefa> OrdenacaoDeTarefa(IEnumerable<Tarefa> tarefas);

    public Tarefa AdicionarTarefa(string titulo, string descricao, Prioridade prioridade,
        string categoria, string responsavel, DateTime? prazo = null)
    {
        var tarefa = new Tarefa(titulo, descricao, prioridade, categoria, responsavel, prazo);

        // Subscrever eventos
        tarefa.StatusAlterado += t =>
            Console.WriteLine($"  📋 Status atualizado: #{t.Id} '{t.Titulo}' → {t.Status}");
        tarefa.PrazoAtrasado += t =>
            Console.WriteLine($"  ⚠️  ATENÇÃO: Tarefa #{t.Id} está atrasada {t.DiasAtraso} dia(s)!");

        _tarefas.Add(tarefa);
        Console.WriteLine($"✓ Tarefa adicionada: #{tarefa.Id:D4} — {tarefa.Titulo}");
        return tarefa;
    }

    public Tarefa? BuscarPorId(int id) => _tarefas.FirstOrDefault(t => t.Id == id);

    public List<Tarefa> Buscar(FiltroDeTarefa filtro) => _tarefas.Where(filtro).ToList();

    public List<Tarefa> BuscarPorCategoria(string categoria) =>
        _tarefas.Where(t => t.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase)).ToList();

    public List<Tarefa> BuscarPorResponsavel(string responsavel) =>
        _tarefas.Where(t => t.Responsavel.Equals(responsavel, StringComparison.OrdinalIgnoreCase)).ToList();

    public List<Tarefa> TarefasAtrasadas() => _tarefas.Where(t => t.EstaAtrasada).ToList();

    public List<Tarefa> TarefasOrdenadas() => _tarefas.OrderBy(t => t).ToList(); // usa IComparable

    public void RemoverTarefa(int id)
    {
        var t = BuscarPorId(id);
        if (t == null) throw new ArgumentException($"Tarefa #{id} não encontrada.");
        _tarefas.Remove(t);
        Console.WriteLine($"✓ Tarefa #{id} removida.");
    }

    public void ExibirTodasTarefas(string titulo = "TODAS AS TAREFAS")
    {
        Console.WriteLine($"\n{'═',60}");
        Console.WriteLine($"  {titulo} ({_tarefas.Count} tarefas)");
        Console.WriteLine($"{'─',60}");
        foreach (var t in TarefasOrdenadas()) t.ExibirDetalhes();
        Console.WriteLine($"{'═',60}");
    }

    public void ExibirEstatisticas()
    {
        Console.WriteLine($"\n{'═',50}");
        Console.WriteLine("  📊 ESTATÍSTICAS");
        Console.WriteLine($"{'─',50}");
        Console.WriteLine($"  Total:        {_tarefas.Count}");
        Console.WriteLine($"  Pendentes:    {_tarefas.Count(t => t.Status == StatusTarefa.Pendente)}");
        Console.WriteLine($"  Em progresso: {_tarefas.Count(t => t.Status == StatusTarefa.EmProgresso)}");
        Console.WriteLine($"  Concluídas:   {_tarefas.Count(t => t.Status == StatusTarefa.Concluida)}");
        Console.WriteLine($"  Canceladas:   {_tarefas.Count(t => t.Status == StatusTarefa.Cancelada)}");
        Console.WriteLine($"  Atrasadas:    {TarefasAtrasadas().Count}");

        if (_tarefas.Any())
        {
            var taxa = (double)_tarefas.Count(t => t.Status == StatusTarefa.Concluida) / _tarefas.Count * 100;
            Console.WriteLine($"\n  Taxa de conclusão: {taxa:F1}%");

            Console.WriteLine("\n  Por categoria:");
            var porCategoria = _tarefas.GroupBy(t => t.Categoria)
                .OrderByDescending(g => g.Count());
            foreach (var g in porCategoria)
                Console.WriteLine($"    {g.Key}: {g.Count()} ({g.Count(t => t.Status == StatusTarefa.Concluida)} concluídas)");
        }
        Console.WriteLine($"{'═',50}");
    }
}

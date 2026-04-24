// Avançado 02 — Repositório Genérico — SOLUÇÃO

interface IEntidade { int Id { get; set; } }

interface IRepositorio<T> where T : IEntidade
{
    T Adicionar(T item);
    T? BuscarPorId(int id);
    IEnumerable<T> ListarTodos();
    T Atualizar(T item);
    bool Remover(int id);
    int Count { get; }
    IEnumerable<T> Buscar(Func<T, bool> filtro);
    (IEnumerable<T> Items, int TotalPaginas) Paginar(int pagina, int tamanho);
}

class RepositorioEmMemoria<T> : IRepositorio<T> where T : IEntidade
{
    private readonly Dictionary<int, T> _dados = new();
    private int _proximoId = 1;

    public int Count => _dados.Count;

    public T Adicionar(T item)
    {
        item.Id = _proximoId++;
        _dados[item.Id] = item;
        return item;
    }

    public T? BuscarPorId(int id) => _dados.TryGetValue(id, out T? item) ? item : default;

    public IEnumerable<T> ListarTodos() => _dados.Values.ToList();

    public T Atualizar(T item)
    {
        if (!_dados.ContainsKey(item.Id)) throw new KeyNotFoundException($"Id {item.Id} não encontrado.");
        _dados[item.Id] = item;
        return item;
    }

    public bool Remover(int id) => _dados.Remove(id);

    public IEnumerable<T> Buscar(Func<T, bool> filtro) => _dados.Values.Where(filtro);

    public (IEnumerable<T> Items, int TotalPaginas) Paginar(int pagina, int tamanho)
    {
        int total = (int)Math.Ceiling((double)Count / tamanho);
        var items = _dados.Values.Skip((pagina - 1) * tamanho).Take(tamanho);
        return (items, total);
    }
}

class Produto : IEntidade
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public double Preco { get; set; }
    public string Categoria { get; set; } = "";
    public override string ToString() => $"[{Id}] {Nome} — R${Preco:F2} ({Categoria})";
}

class Programa
{
    static void Main()
    {
        var repo = new RepositorioEmMemoria<Produto>();

        repo.Adicionar(new Produto { Nome = "Notebook", Preco = 3500, Categoria = "Eletrônicos" });
        repo.Adicionar(new Produto { Nome = "Mouse", Preco = 80, Categoria = "Eletrônicos" });
        repo.Adicionar(new Produto { Nome = "Cadeira", Preco = 1200, Categoria = "Móveis" });
        repo.Adicionar(new Produto { Nome = "Mesa", Preco = 800, Categoria = "Móveis" });
        repo.Adicionar(new Produto { Nome = "Teclado", Preco = 150, Categoria = "Eletrônicos" });

        Console.WriteLine("=== Todos os produtos ===");
        foreach (var p in repo.ListarTodos()) Console.WriteLine($"  {p}");

        Console.WriteLine("\n=== Eletrônicos ===");
        foreach (var p in repo.Buscar(p => p.Categoria == "Eletrônicos")) Console.WriteLine($"  {p}");

        Console.WriteLine("\n=== Página 1 (2 por página) ===");
        var (items, totalPags) = repo.Paginar(1, 2);
        foreach (var p in items) Console.WriteLine($"  {p}");
        Console.WriteLine($"Total de páginas: {totalPags}");

        Console.WriteLine("\n=== Busca por id 3 ===");
        Console.WriteLine(repo.BuscarPorId(3));
    }
}

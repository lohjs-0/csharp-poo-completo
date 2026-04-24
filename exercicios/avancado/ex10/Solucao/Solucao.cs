// Avançado 10 — Mini ORM com Reflection — SOLUÇÃO

using System.Reflection;

// Custom Attributes
[AttributeUsage(AttributeTargets.Class)]
class TabelaAttribute : Attribute
{
    public string Nome { get; }
    public TabelaAttribute(string nome) { Nome = nome; }
}

[AttributeUsage(AttributeTargets.Property)]
class ColunaAttribute : Attribute
{
    public string Nome { get; }
    public ColunaAttribute(string nome) { Nome = nome; }
}

[AttributeUsage(AttributeTargets.Property)]
class ChavePrimariaAttribute : Attribute { }

// Entidades mapeadas
[Tabela("produtos")]
class ProdutoORM
{
    [ChavePrimaria, Coluna("id")] public int Id { get; set; }
    [Coluna("nome")] public string Nome { get; set; } = "";
    [Coluna("preco")] public double Preco { get; set; }
    [Coluna("categoria")] public string Categoria { get; set; } = "";
}

[Tabela("clientes")]
class ClienteORM
{
    [ChavePrimaria, Coluna("id")] public int Id { get; set; }
    [Coluna("nome")] public string Nome { get; set; } = "";
    [Coluna("email")] public string Email { get; set; } = "";
}

class MiniORM
{
    private static string ObterTabela<T>() =>
        typeof(T).GetCustomAttribute<TabelaAttribute>()?.Nome ?? typeof(T).Name.ToLower();

    private static string GerarInsert<T>(T entidade)
    {
        var tipo = typeof(T);
        var tabela = ObterTabela<T>();
        var props = tipo.GetProperties()
            .Where(p => p.GetCustomAttribute<ColunaAttribute>() != null &&
                        p.GetCustomAttribute<ChavePrimariaAttribute>() == null);

        var colunas = string.Join(", ", props.Select(p => p.GetCustomAttribute<ColunaAttribute>()!.Nome));
        var valores = string.Join(", ", props.Select(p =>
        {
            var val = p.GetValue(entidade);
            return val is string ? $"'{val}'" : val?.ToString() ?? "NULL";
        }));

        return $"INSERT INTO {tabela} ({colunas}) VALUES ({valores})";
    }

    private static string GerarSelect<T>(int? id = null)
    {
        var tabela = ObterTabela<T>();
        return id.HasValue ? $"SELECT * FROM {tabela} WHERE id = {id}" : $"SELECT * FROM {tabela}";
    }

    public T Salvar<T>(T entidade) where T : class
    {
        string sql = GerarInsert(entidade);
        Console.WriteLine($"[ORM] Executando: {sql}");
        return entidade;
    }

    public string Buscar<T>(int id) => GerarSelect<T>(id);
    public string ListarTodos<T>() => GerarSelect<T>();
}

class Programa
{
    static void Main()
    {
        var orm = new MiniORM();

        var produto = new ProdutoORM { Nome = "Notebook", Preco = 3500, Categoria = "Eletrônicos" };
        orm.Salvar(produto);

        var cliente = new ClienteORM { Nome = "Ana Silva", Email = "ana@email.com" };
        orm.Salvar(cliente);

        Console.WriteLine($"\n[ORM] {orm.Buscar<ProdutoORM>(1)}");
        Console.WriteLine($"[ORM] {orm.ListarTodos<ClienteORM>()}");
    }
}

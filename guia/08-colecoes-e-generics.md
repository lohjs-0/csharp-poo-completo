# 08 — Coleções e Generics

## Generics: O que são?

**Generics** permitem criar classes, métodos e interfaces que funcionam com **qualquer tipo**, sem perda de segurança de tipos.

```csharp
// Sem generics — perde tipo, usa boxing/unboxing, propenso a erros
ArrayList lista = new ArrayList();
lista.Add(42);
lista.Add("texto"); // aceita qualquer coisa — perigoso!
int numero = (int)lista[0]; // cast necessário

// Com generics — tipado e seguro!
List<int> listaInt = new List<int>();
listaInt.Add(42);
// listaInt.Add("texto"); // ERRO de compilação — correto!
int numero = listaInt[0]; // sem cast necessário
```

---

## Criando Classes Genéricas

```csharp
// Classe genérica — T é o tipo "parâmetro"
class Caixa<T>
{
    private T _conteudo;
    public bool EstaVazia { get; private set; } = true;

    public void Colocar(T item)
    {
        _conteudo = item;
        EstaVazia = false;
        Console.WriteLine($"Item colocado na caixa: {item}");
    }

    public T Retirar()
    {
        if (EstaVazia)
            throw new InvalidOperationException("A caixa está vazia!");
        EstaVazia = true;
        return _conteudo;
    }

    public T Espiar()
    {
        if (EstaVazia)
            throw new InvalidOperationException("A caixa está vazia!");
        return _conteudo;
    }
}

// Uso com diferentes tipos:
var caixaNumero = new Caixa<int>();
caixaNumero.Colocar(42);
int numero = caixaNumero.Retirar(); // sem cast!

var caixaTexto = new Caixa<string>();
caixaTexto.Colocar("Presente surpresa!");
string texto = caixaTexto.Retirar();

var caixaPessoa = new Caixa<Pessoa>(); // funciona com qualquer tipo!
```

---

## Múltiplos Parâmetros de Tipo

```csharp
class Par<TPrimeiro, TSegundo>
{
    public TPrimeiro Primeiro { get; }
    public TSegundo Segundo { get; }

    public Par(TPrimeiro primeiro, TSegundo segundo)
    {
        Primeiro = primeiro;
        Segundo = segundo;
    }

    public override string ToString() => $"({Primeiro}, {Segundo})";
}

// Uso:
var parIdadeNome = new Par<int, string>(25, "Ana");
var parCoordenada = new Par<double, double>(40.7128, -74.0060);
var parResultado = new Par<bool, string>(true, "Aprovado");

Console.WriteLine(parIdadeNome);   // (25, Ana)
Console.WriteLine(parCoordenada);  // (40.7128, -74.006)
```

---

## Constraints (Restrições de Tipo)

Você pode restringir quais tipos são aceitos:

```csharp
// where T : class — T deve ser um tipo referência
class Repositorio<T> where T : class
{
    private List<T> _items = new();
    public void Adicionar(T item) => _items.Add(item);
}

// where T : struct — T deve ser um tipo valor
class Opcional<T> where T : struct
{
    private T? _valor;
    public bool TemValor => _valor.HasValue;
    public void Definir(T valor) => _valor = valor;
    public T Obter() => _valor ?? default;
}

// where T : new() — T deve ter construtor sem parâmetros
class Fabrica<T> where T : new()
{
    public T Criar() => new T();
}

// where T : Animal — T deve ser Animal ou derivado
class Zoologico<T> where T : Animal
{
    private List<T> _animais = new();
    public void AdicionarAnimal(T animal) => _animais.Add(animal);
    public void FazerTodosEmitirSom() => _animais.ForEach(a => a.FazerSom());
}

// Múltiplas constraints
class Repositorio<T> where T : class, IEntidade, new()
{
    // T deve ser: classe, implementar IEntidade e ter construtor sem parâmetros
}
```

---

## Métodos Genéricos

```csharp
class Utilitarios
{
    // Método genérico
    public static void Trocar<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }

    public static T Maximo<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b) >= 0 ? a : b;
    }

    public static List<T> Filtrar<T>(List<T> lista, Func<T, bool> predicado)
    {
        return lista.Where(predicado).ToList();
    }
}

// Uso:
int x = 5, y = 10;
Utilitarios.Trocar(ref x, ref y);
Console.WriteLine($"x={x}, y={y}"); // x=10, y=5

double maior = Utilitarios.Maximo(3.14, 2.71); // 3.14
string maiorTexto = Utilitarios.Maximo("banana", "abacaxi"); // banana

var numeros = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
var pares = Utilitarios.Filtrar(numeros, n => n % 2 == 0);
// pares = [2, 4, 6, 8, 10]
```

---

## Coleções Genéricas Essenciais

### List\<T\>
```csharp
var frutas = new List<string> { "maçã", "banana", "laranja" };

frutas.Add("uva");
frutas.AddRange(new[] { "manga", "melão" });
frutas.Remove("banana");
frutas.Insert(0, "abacaxi"); // insere no índice 0

Console.WriteLine(frutas.Count);       // quantidade
Console.WriteLine(frutas.Contains("uva")); // true
Console.WriteLine(frutas[0]);          // abacaxi

// LINQ com List
var ordenada = frutas.OrderBy(f => f).ToList();
var comA = frutas.Where(f => f.StartsWith("a")).ToList();
var primeiraFruta = frutas.FirstOrDefault();
```

### Dictionary<TKey, TValue>
```csharp
var capitais = new Dictionary<string, string>
{
    { "Brasil", "Brasília" },
    { "França", "Paris" },
    ["Japão"] = "Tóquio" // sintaxe alternativa
};

capitais["Alemanha"] = "Berlim"; // adicionar
capitais["Brasil"] = "Brasília"; // atualizar

// Verificar existência
if (capitais.ContainsKey("França"))
    Console.WriteLine(capitais["França"]);

// TryGetValue — mais seguro (sem exceção se chave não existe)
if (capitais.TryGetValue("Espanha", out string? capital))
    Console.WriteLine(capital);
else
    Console.WriteLine("País não encontrado");

// Iterar
foreach (var (pais, cap) in capitais)
    Console.WriteLine($"{pais}: {cap}");
```

### Queue\<T\> (Fila — FIFO)
```csharp
var fila = new Queue<string>();
fila.Enqueue("Primeiro");
fila.Enqueue("Segundo");
fila.Enqueue("Terceiro");

Console.WriteLine(fila.Peek());    // "Primeiro" — espia sem remover
Console.WriteLine(fila.Dequeue()); // "Primeiro" — remove e retorna
Console.WriteLine(fila.Count);     // 2
```

### Stack\<T\> (Pilha — LIFO)
```csharp
var historico = new Stack<string>();
historico.Push("página1.html");
historico.Push("página2.html");
historico.Push("página3.html");

Console.WriteLine(historico.Peek());    // "página3.html" — topo
Console.WriteLine(historico.Pop());     // "página3.html" — remove topo
Console.WriteLine(historico.Peek());    // "página2.html" — novo topo
```

### HashSet\<T\> (Conjunto sem duplicatas)
```csharp
var tags = new HashSet<string> { "csharp", "dotnet", "programacao" };
tags.Add("csharp");  // não adiciona — já existe!
tags.Add("poo");     // adiciona

Console.WriteLine(tags.Count); // 4

var outrasTags = new HashSet<string> { "dotnet", "web", "api" };

// Operações de conjunto
var uniao = new HashSet<string>(tags);
uniao.UnionWith(outrasTags); // união

var intersecao = new HashSet<string>(tags);
intersecao.IntersectWith(outrasTags); // intersecção
```

---

## Exemplo: Repositório Genérico

```csharp
interface IEntidade
{
    int Id { get; }
}

class Produto : IEntidade
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public double Preco { get; set; }
}

class Cliente : IEntidade
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string Email { get; set; } = "";
}

class Repositorio<T> where T : class, IEntidade
{
    private readonly Dictionary<int, T> _dados = new();
    private int _proximoId = 1;

    public T Adicionar(T item)
    {
        // Usando reflection para definir o Id
        var prop = typeof(T).GetProperty("Id");
        prop?.SetValue(item, _proximoId++);

        _dados[((IEntidade)item).Id] = item;
        return item;
    }

    public T? BuscarPorId(int id)
    {
        return _dados.TryGetValue(id, out T? item) ? item : null;
    }

    public IEnumerable<T> ListarTodos() => _dados.Values;

    public bool Remover(int id) => _dados.Remove(id);

    public int Count => _dados.Count;
}

// Uso:
var repoProdutos = new Repositorio<Produto>();
var repoClientes = new Repositorio<Cliente>();

repoProdutos.Adicionar(new Produto { Nome = "Notebook", Preco = 3500 });
repoProdutos.Adicionar(new Produto { Nome = "Mouse", Preco = 80 });
repoClientes.Adicionar(new Cliente { Nome = "Ana", Email = "ana@email.com" });

Console.WriteLine($"Produtos: {repoProdutos.Count}");
Console.WriteLine($"Clientes: {repoClientes.Count}");

foreach (var p in repoProdutos.ListarTodos())
    Console.WriteLine($"  [{p.Id}] {p.Nome}: R${p.Preco}");
```

---

## Resumo

| Coleção | Uso principal | Acesso |
|---------|---------------|--------|
| `List<T>` | Lista ordenada e indexada | O(1) por índice |
| `Dictionary<K,V>` | Pares chave-valor | O(1) por chave |
| `HashSet<T>` | Conjunto sem duplicatas | O(1) para busca |
| `Queue<T>` | Fila (primeiro a entrar, primeiro a sair) | FIFO |
| `Stack<T>` | Pilha (último a entrar, primeiro a sair) | LIFO |

---

## Exercícios desta seção

- [Exercício 08 — Coleção Genérica de Alunos](../exercicios/intermediario/ex08/README.md)
- [Exercício 02 — Repositório Genérico](../exercicios/avancado/ex02/README.md)

---

➡️ [Guia 09 — Tratamento de Exceções](09-excecoes.md)

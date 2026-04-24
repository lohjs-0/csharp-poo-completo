# 07 — Interfaces

## O que é uma Interface?

Uma **interface** é um contrato que define **o que** uma classe deve fazer, sem dizer **como** ela deve fazer.

- Define apenas **assinaturas** de métodos e propriedades
- Não tem implementação (exceto membros default a partir do C# 8)
- Uma classe pode implementar **múltiplas interfaces** (C# não tem herança múltipla de classes)
- Representa a relação **"PODE FAZER"** ou **"SE COMPORTA COMO"**

---

## Sintaxe Básica

```csharp
// Declaração da interface (convenção: começa com "I")
interface IDescricao
{
    string ObterDescricao();  // sem corpo
}

interface ISalvavel
{
    bool Salvar();
    bool Carregar(int id);
}

// Classe implementando múltiplas interfaces
class Produto : IDescricao, ISalvavel
{
    public string Nome { get; set; }
    public double Preco { get; set; }

    // Implementação obrigatória de IDescricao
    public string ObterDescricao()
    {
        return $"{Nome} - R${Preco:F2}";
    }

    // Implementações obrigatórias de ISalvavel
    public bool Salvar()
    {
        Console.WriteLine($"Produto {Nome} salvo.");
        return true;
    }

    public bool Carregar(int id)
    {
        Console.WriteLine($"Carregando produto {id}...");
        return true;
    }
}
```

---

## Interface vs Classe Abstrata

| | Interface | Classe Abstrata |
|---|---|---|
| Instanciável | Não | Não |
| Implementação | Não (padrão) | Sim (métodos concretos) |
| Herança múltipla | Sim (múltiplas) | Não (apenas uma) |
| Campos | Não | Sim |
| Construtores | Não | Sim |
| Relação | "Pode fazer" | "É um" |

---

## Exemplo: Interfaces Comuns do C#

O C# tem várias interfaces nativas que você pode implementar:

```csharp
// IComparable: permite comparar objetos (para ordenação)
class Produto : IComparable<Produto>
{
    public string Nome { get; set; }
    public double Preco { get; set; }

    public int CompareTo(Produto? outro)
    {
        if (outro == null) return 1;
        return Preco.CompareTo(outro.Preco); // ordena por preço
    }
}

// IEnumerable: permite usar foreach
class Catalogo : IEnumerable<Produto>
{
    private List<Produto> _produtos = new();

    public void Adicionar(Produto p) => _produtos.Add(p);

    public IEnumerator<Produto> GetEnumerator() => _produtos.GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();
}

// Uso:
var catalogo = new Catalogo();
catalogo.Adicionar(new Produto { Nome = "Mouse", Preco = 80 });
catalogo.Adicionar(new Produto { Nome = "Teclado", Preco = 150 });
catalogo.Adicionar(new Produto { Nome = "Monitor", Preco = 900 });

foreach (var p in catalogo) // funciona por causa do IEnumerable
    Console.WriteLine(p.Nome);

var lista = new List<Produto> { /* ... */ };
lista.Sort(); // funciona por causa do IComparable
```

---

## Exemplo Completo: Sistema de Notificações

```csharp
// Interface central
interface INotificador
{
    string Canal { get; }
    bool Enviar(string destinatario, string titulo, string mensagem);
}

// Diferentes implementações do mesmo contrato
class NotificadorEmail : INotificador
{
    public string Canal => "E-mail";

    public bool Enviar(string destinatario, string titulo, string mensagem)
    {
        Console.WriteLine($"[EMAIL] Para: {destinatario}");
        Console.WriteLine($"  Assunto: {titulo}");
        Console.WriteLine($"  Corpo: {mensagem}");
        // lógica real de envio de email aqui
        return true;
    }
}

class NotificadorSMS : INotificador
{
    public string Canal => "SMS";

    public bool Enviar(string destinatario, string titulo, string mensagem)
    {
        // SMS é limitado a 160 caracteres
        string sms = $"{titulo}: {mensagem}";
        if (sms.Length > 160) sms = sms[..157] + "...";

        Console.WriteLine($"[SMS] Para: {destinatario}");
        Console.WriteLine($"  Mensagem: {sms}");
        return true;
    }
}

class NotificadorPush : INotificador
{
    public string Canal => "Push Notification";

    public bool Enviar(string destinatario, string titulo, string mensagem)
    {
        Console.WriteLine($"[PUSH] Dispositivo: {destinatario}");
        Console.WriteLine($"  Título: {titulo}");
        Console.WriteLine($"  Texto: {mensagem}");
        return true;
    }
}

// Serviço que usa a interface — não sabe o tipo concreto!
class ServicoNotificacao
{
    private readonly List<INotificador> _notificadores;

    public ServicoNotificacao(params INotificador[] notificadores)
    {
        _notificadores = notificadores.ToList();
    }

    public void AdicionarCanal(INotificador notificador)
    {
        _notificadores.Add(notificador);
    }

    // Dispara a notificação em TODOS os canais cadastrados
    public void NotificarTodos(string destinatario, string titulo, string mensagem)
    {
        Console.WriteLine($"=== Enviando notificação para {destinatario} ===");
        foreach (var notificador in _notificadores)
        {
            bool sucesso = notificador.Enviar(destinatario, titulo, mensagem);
            Console.WriteLine($"  [{notificador.Canal}]: {(sucesso ? "✓" : "✗")}");
        }
    }
}

// Uso:
var servico = new ServicoNotificacao(
    new NotificadorEmail(),
    new NotificadorSMS()
);

servico.AdicionarCanal(new NotificadorPush());

servico.NotificarTodos(
    "usuario@email.com",
    "Pedido confirmado",
    "Seu pedido #1234 foi confirmado e está sendo preparado."
);
```

---

## Interfaces com Propriedades e Eventos

```csharp
interface IRepositorio<T>
{
    // Propriedade
    int Count { get; }

    // Métodos CRUD
    void Adicionar(T item);
    void Remover(T item);
    T? BuscarPorId(int id);
    IEnumerable<T> ListarTodos();
    void Atualizar(T item);
}

// Implementação em memória (para testes)
class RepositorioEmMemoria<T> : IRepositorio<T> where T : class
{
    private readonly List<T> _items = new();

    public int Count => _items.Count;

    public void Adicionar(T item) => _items.Add(item);

    public void Remover(T item) => _items.Remove(item);

    public T? BuscarPorId(int id)
    {
        // Simplificado — assumindo que T tem propriedade Id
        return _items.ElementAtOrDefault(id);
    }

    public IEnumerable<T> ListarTodos() => _items.AsReadOnly();

    public void Atualizar(T item)
    {
        // Simplificado
        Console.WriteLine($"Item atualizado: {item}");
    }
}
```

---

## Implementação Explícita de Interface

Quando duas interfaces têm membros com o mesmo nome:

```csharp
interface IImpressora
{
    void Imprimir();
}

interface IScanner
{
    void Imprimir(); // mesmo nome, contexto diferente
}

class MultiFuncional : IImpressora, IScanner
{
    // Implementação explícita — só acessível pelo tipo da interface
    void IImpressora.Imprimir()
    {
        Console.WriteLine("Imprimindo documento...");
    }

    void IScanner.Imprimir()
    {
        Console.WriteLine("Imprimindo scan digitalizado...");
    }
}

var mf = new MultiFuncional();
// mf.Imprimir(); // ERRO — ambíguo!

((IImpressora)mf).Imprimir(); // "Imprimindo documento..."
((IScanner)mf).Imprimir();    // "Imprimindo scan digitalizado..."
```

---

## Interfaces com Implementação Default (C# 8+)

```csharp
interface ILogger
{
    void Log(string mensagem);
    void LogErro(string mensagem);

    // Implementação default — opcional para as classes
    void LogInfo(string mensagem)
    {
        Log($"[INFO] {mensagem}");
    }

    void LogAviso(string mensagem)
    {
        Log($"[AVISO] {mensagem}");
    }
}

class ConsoleLogger : ILogger
{
    public void Log(string mensagem) => Console.WriteLine(mensagem);
    public void LogErro(string mensagem) => Console.WriteLine($"[ERRO] {mensagem}");
    // LogInfo e LogAviso são herdados com implementação default!
}
```

---

## Boas Práticas com Interfaces

```csharp
// ✅ Interfaces pequenas e focadas (Princípio da Segregação de Interface)
interface IPagavel { double CalcularValor(); }
interface IImprimivel { string GerarRelatorio(); }
interface INotificavel { void Notificar(string mensagem); }

// Em vez de uma interface gigante:
// ❌ interface IGigante { double Calcular(); string Gerar(); void Notificar(); ... }

// ✅ Dependência via interface, não via implementação concreta
class Pedido
{
    private readonly INotificador _notificador; // interface!

    public Pedido(INotificador notificador) // injeção de dependência
    {
        _notificador = notificador;
    }

    public void Confirmar()
    {
        // lógica de confirmação...
        _notificador.Enviar("cliente", "Pedido confirmado", "Seu pedido foi confirmado.");
    }
}
```

---

## Resumo

| Conceito | Descrição |
|----------|-----------|
| `interface` | Contrato com assinaturas de membros |
| Implementação | `class Foo : IBar { }` |
| Múltiplas interfaces | `class Foo : IBar, IBaz, IQux { }` |
| Sem estado | Interfaces não têm campos |
| Injeção de dependência | Programar para interfaces, não implementações |

---

## Exercícios desta seção

- [Exercício 04 — Interface: IPagavel](../exercicios/intermediario/ex04/README.md)
- [Exercício 06 — Interface: IComparavel](../exercicios/intermediario/ex06/README.md)

---

➡️ [Guia 08 — Coleções e Generics](08-colecoes-e-generics.md)

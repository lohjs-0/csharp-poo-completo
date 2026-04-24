# 10 — Boas Práticas em POO com C#

## Os Princípios SOLID

**SOLID** é um conjunto de 5 princípios para escrever código orientado a objetos de qualidade.

---

### S — Single Responsibility Principle (Responsabilidade Única)

> Uma classe deve ter apenas **uma razão para mudar**.

```csharp
// ❌ Classe com múltiplas responsabilidades
class PedidoRuim
{
    public void CriarPedido() { /* ... */ }
    public void SalvarNoBanco() { /* ... */ } // responsabilidade do banco!
    public void EnviarEmailConfirmacao() { /* ... */ } // responsabilidade de email!
    public void GerarRelatorio() { /* ... */ } // responsabilidade de relatório!
}

// ✅ Cada classe com uma responsabilidade
class Pedido
{
    public int Id { get; set; }
    public List<ItemPedido> Itens { get; } = new();
    public double Total => Itens.Sum(i => i.Subtotal);

    public void AdicionarItem(ItemPedido item) => Itens.Add(item);
}

class PedidoRepositorio
{
    public void Salvar(Pedido pedido) { /* salva no banco */ }
    public Pedido? BuscarPorId(int id) { return null; /* ... */ }
}

class EmailService
{
    public void EnviarConfirmacao(Pedido pedido) { /* envia email */ }
}

class RelatorioService
{
    public string GerarRelatorio(Pedido pedido) { return ""; /* ... */ }
}
```

---

### O — Open/Closed Principle (Aberto/Fechado)

> Uma classe deve ser **aberta para extensão**, mas **fechada para modificação**.

```csharp
// ❌ Precisa modificar a classe para adicionar novo tipo
class CalculadoraDescontoRuim
{
    public double CalcularDesconto(string tipoCliente, double valor)
    {
        if (tipoCliente == "VIP")
            return valor * 0.20;
        else if (tipoCliente == "Regular")
            return valor * 0.10;
        // Para adicionar "Premium", teria que modificar aqui! ❌
        return 0;
    }
}

// ✅ Extensível sem modificar o código existente
interface IEstrategiaDesconto
{
    double CalcularDesconto(double valor);
}

class DescontoVIP : IEstrategiaDesconto
{
    public double CalcularDesconto(double valor) => valor * 0.20;
}

class DescontoRegular : IEstrategiaDesconto
{
    public double CalcularDesconto(double valor) => valor * 0.10;
}

class DescontoPremium : IEstrategiaDesconto // nova classe, sem modificar as existentes!
{
    public double CalcularDesconto(double valor) => valor * 0.30;
}

class CalculadoraDesconto
{
    public double Calcular(IEstrategiaDesconto estrategia, double valor)
    {
        return estrategia.CalcularDesconto(valor);
    }
}
```

---

### L — Liskov Substitution Principle (Substituição de Liskov)

> Objetos de classes filhas devem poder **substituir** objetos da classe pai sem quebrar o programa.

```csharp
// ❌ Viola Liskov — Pinguim herda de Ave, mas não pode voar!
class Ave
{
    public virtual void Voar()
    {
        Console.WriteLine("Estou voando!");
    }
}

class Pinguim : Ave
{
    public override void Voar()
    {
        throw new NotSupportedException("Pinguins não voam!"); // quebra o contrato!
    }
}

// ✅ Respeita Liskov — separar comportamentos diferentes
class Ave
{
    public string Nome { get; set; }
    public virtual void Mover() { Console.WriteLine("Me movendo..."); }
}

interface IVoador
{
    void Voar();
}

class Pardal : Ave, IVoador
{
    public void Voar() => Console.WriteLine("Pardal voando!");
    public override void Mover() => Voar();
}

class Pinguim : Ave  // não implementa IVoador
{
    public void Nadar() => Console.WriteLine("Pinguim nadando!");
    public override void Mover() => Nadar();
}
```

---

### I — Interface Segregation Principle (Segregação de Interface)

> Clientes não devem ser **forçados a depender** de interfaces que não usam.

```csharp
// ❌ Interface "gorda" — nem todo dispositivo faz tudo
interface IDispositivoRuim
{
    void Imprimir();
    void Escanear();
    void Enviar();
    void Fotocopiar();
}

class ImpressoraSimples : IDispositivoRuim
{
    public void Imprimir() { /* ok */ }
    public void Escanear() => throw new NotImplementedException(); // ❌
    public void Enviar() => throw new NotImplementedException(); // ❌
    public void Fotocopiar() => throw new NotImplementedException(); // ❌
}

// ✅ Interfaces pequenas e focadas
interface IImpressora { void Imprimir(); }
interface IScanner { void Escanear(); }
interface IFax { void Enviar(); }

class ImpressoraSimplesBoa : IImpressora
{
    public void Imprimir() { Console.WriteLine("Imprimindo..."); }
}

class MultiFuncional : IImpressora, IScanner, IFax
{
    public void Imprimir() { Console.WriteLine("Imprimindo..."); }
    public void Escanear() { Console.WriteLine("Escaneando..."); }
    public void Enviar() { Console.WriteLine("Enviando fax..."); }
}
```

---

### D — Dependency Inversion Principle (Inversão de Dependência)

> Módulos de alto nível não devem depender de módulos de baixo nível. **Ambos devem depender de abstrações**.

```csharp
// ❌ Dependência direta da implementação concreta
class PedidoServiceRuim
{
    private MySQLBanco _banco = new MySQLBanco(); // acoplado ao MySQL!

    public void Salvar(Pedido p) => _banco.Inserir(p);
}

// ✅ Depende da abstração (interface), não da implementação
interface IBancoRepositorio
{
    void Salvar(Pedido pedido);
    Pedido? Buscar(int id);
}

class MySQLRepositorio : IBancoRepositorio
{
    public void Salvar(Pedido pedido) { /* MySQL */ }
    public Pedido? Buscar(int id) { return null; /* MySQL */ }
}

class SQLiteRepositorio : IBancoRepositorio // troca sem mudar PedidoService!
{
    public void Salvar(Pedido pedido) { /* SQLite */ }
    public Pedido? Buscar(int id) { return null; /* SQLite */ }
}

class PedidoService // não sabe qual banco está usando!
{
    private readonly IBancoRepositorio _repositorio;

    public PedidoService(IBancoRepositorio repositorio) // injetado!
    {
        _repositorio = repositorio;
    }

    public void ProcessarPedido(Pedido pedido)
    {
        // lógica de negócio...
        _repositorio.Salvar(pedido);
    }
}

// Na composição:
var service = new PedidoService(new MySQLRepositorio());
// ou:
var serviceTest = new PedidoService(new SQLiteRepositorio());
```

---

## Convenções de Nomenclatura em C#

```csharp
// Classes, Structs, Enums, Interfaces: PascalCase
class MinhaClasse { }
interface IMinhaInterface { }
enum DiaDaSemana { Segunda, Terca, Quarta }

// Métodos e Propriedades: PascalCase
public void CalcularTotal() { }
public string NomeCompleto { get; set; }

// Parâmetros e variáveis locais: camelCase
public void Calcular(double valorBase, int quantidade)
{
    double resultado = valorBase * quantidade;
}

// Campos privados: _camelCase com underscore
private double _saldo;
private readonly string _nome;

// Constantes: PascalCase ou SCREAMING_SNAKE_CASE
public const double Pi = 3.14159;
private const int MAX_TENTATIVAS = 3;
```

---

## Composição sobre Herança

Prefira **composição** (ter) a **herança** (ser), quando possível:

```csharp
// ❌ Herança para reutilizar código (errado)
class Logger
{
    protected void Log(string msg) => Console.WriteLine($"[LOG] {msg}");
}

class PedidoService : Logger // Pedido não "É um" Logger!
{
    public void Criar() { Log("Pedido criado"); }
}

// ✅ Composição — injeta o logger
class PedidoServiceBom
{
    private readonly ILogger _logger;

    public PedidoServiceBom(ILogger logger)
    {
        _logger = logger;
    }

    public void Criar()
    {
        // lógica...
        _logger.Log("Pedido criado");
    }
}
```

---

## Imutabilidade

Prefira objetos imutáveis quando possível — mais seguros e previsíveis:

```csharp
// Classe imutável — uma vez criada, não muda
class Dinheiro
{
    public decimal Valor { get; }      // somente leitura!
    public string Moeda { get; }

    public Dinheiro(decimal valor, string moeda)
    {
        if (valor < 0)
            throw new ArgumentException("Valor não pode ser negativo.");
        Valor = valor;
        Moeda = moeda;
    }

    // Em vez de modificar, retorna um NOVO objeto
    public Dinheiro Somar(Dinheiro outro)
    {
        if (Moeda != outro.Moeda)
            throw new InvalidOperationException("Moedas diferentes.");
        return new Dinheiro(Valor + outro.Valor, Moeda);
    }

    public override string ToString() => $"{Moeda} {Valor:F2}";
}

var preco = new Dinheiro(100, "BRL");
var desconto = new Dinheiro(15, "BRL");
var total = preco.Somar(desconto); // novo objeto!
Console.WriteLine(total); // BRL 115.00
Console.WriteLine(preco); // BRL 100.00 — não mudou!
```

---

## Resumo dos Princípios SOLID

| Princípio | Frase resumo |
|-----------|-------------|
| **S**ingle Responsibility | Uma classe, uma razão para mudar |
| **O**pen/Closed | Aberto para extensão, fechado para modificação |
| **L**iskov Substitution | Filhas devem poder substituir a mãe |
| **I**nterface Segregation | Muitas interfaces pequenas > uma grande |
| **D**ependency Inversion | Dependa de abstrações, não de implementações |

---

## Parabéns! 🎉

Você chegou ao fim do guia teórico. Agora é hora de praticar!

- 💪 [Faça os exercícios avançados](../exercicios/avancado/)
- 🚀 [Construa os projetos completos](../projetos/)

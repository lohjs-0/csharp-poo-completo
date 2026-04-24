# 03 — Encapsulamento

## O que é Encapsulamento?

**Encapsulamento** é o princípio de **esconder os detalhes internos** de um objeto e expor apenas o que é necessário para o mundo externo.

Pense em uma **televisão**: você tem botões de volume e canal. Você não precisa saber como os circuitos funcionam internamente — eles estão encapsulados. Se algo muda internamente, você não precisa reaprender a usar a TV.

---

## Modificadores de Acesso

C# tem 5 modificadores de acesso:

| Modificador | Acessível por |
|-------------|---------------|
| `public` | Qualquer código |
| `private` | Apenas dentro da própria classe |
| `protected` | Dentro da classe e classes filhas |
| `internal` | Dentro do mesmo assembly (projeto) |
| `protected internal` | Classes filhas ou mesmo assembly |

O mais comum no dia a dia: `public` e `private`.

---

## Problema sem Encapsulamento

```csharp
// ❌ Sem encapsulamento — perigoso!
class ContaSemEncapsulamento
{
    public double Saldo; // qualquer um pode modificar!
    public string Senha; // dado sensível exposto!
}

var conta = new ContaSemEncapsulamento();
conta.Saldo = -99999; // CAOS! Ninguém impediu
conta.Senha = "123";  // dado sensível acessível
```

---

## Solução com Encapsulamento

```csharp
// ✅ Com encapsulamento — seguro e controlado!
class ContaEncapsulada
{
    // Campos privados — ninguém acessa diretamente
    private double _saldo;
    private string _senha;
    private List<string> _historico;

    public string Titular { get; }

    public ContaEncapsulada(string titular, string senha)
    {
        Titular = titular;
        _senha = senha;
        _saldo = 0;
        _historico = new List<string>();
    }

    // Propriedade somente leitura — só mostra, não permite alterar diretamente
    public double Saldo => _saldo;

    // Método controlado para modificar o saldo
    public bool Depositar(double valor)
    {
        if (valor <= 0)
        {
            Console.WriteLine("Valor de depósito deve ser positivo.");
            return false;
        }
        _saldo += valor;
        _historico.Add($"Depósito: +R${valor:F2}");
        return true;
    }

    public bool Sacar(double valor, string senha)
    {
        if (senha != _senha)
        {
            Console.WriteLine("Senha incorreta!");
            return false;
        }
        if (valor <= 0)
        {
            Console.WriteLine("Valor deve ser positivo.");
            return false;
        }
        if (valor > _saldo)
        {
            Console.WriteLine("Saldo insuficiente.");
            return false;
        }
        _saldo -= valor;
        _historico.Add($"Saque: -R${valor:F2}");
        return true;
    }

    public void ExibirHistorico()
    {
        Console.WriteLine($"=== Histórico de {Titular} ===");
        foreach (var item in _historico)
            Console.WriteLine($"  {item}");
        Console.WriteLine($"  Saldo atual: R${_saldo:F2}");
    }
}

// Uso:
var conta = new ContaEncapsulada("Ana", "1234");
conta.Depositar(1000);
conta.Depositar(-50);        // Bloqueado — valor inválido
conta.Sacar(200, "0000");    // Bloqueado — senha errada
conta.Sacar(200, "1234");    // OK
conta.ExibirHistorico();

// Console.WriteLine(conta._saldo); // ERRO DE COMPILAÇÃO — privado!
```

---

## Propriedades em Detalhes

Propriedades são a forma elegante do C# de implementar encapsulamento:

```csharp
class Temperatura
{
    private double _celsius;

    // Propriedade com getter e setter personalizados
    public double Celsius
    {
        get { return _celsius; }
        set
        {
            if (value < -273.15)
                throw new ArgumentException("Temperatura abaixo do zero absoluto!");
            _celsius = value;
        }
    }

    // Propriedade computed (calculada a partir de outra)
    public double Fahrenheit
    {
        get { return _celsius * 9 / 5 + 32; }
        set { Celsius = (value - 32) * 5 / 9; } // converte e armazena como Celsius
    }

    public double Kelvin
    {
        get { return _celsius + 273.15; }
    }

    // Propriedade auto-implementada (C# gera o campo privado automaticamente)
    public string Unidade { get; private set; } = "°C";
}

var temp = new Temperatura();
temp.Celsius = 100;
Console.WriteLine(temp.Fahrenheit); // 212
Console.WriteLine(temp.Kelvin);     // 373.15

temp.Fahrenheit = 32;
Console.WriteLine(temp.Celsius);    // 0 (água congelando)

// temp.Celsius = -300; // lança exceção — protegido!
```

---

## Padrão: Validação no Setter

```csharp
class Aluno
{
    private string _nome;
    private double _media;
    private int _idade;

    public string Nome
    {
        get => _nome;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Nome não pode ser vazio.");
            _nome = value.Trim();
        }
    }

    public double Media
    {
        get => _media;
        set
        {
            if (value < 0 || value > 10)
                throw new ArgumentOutOfRangeException("Média deve ser entre 0 e 10.");
            _media = value;
        }
    }

    public int Idade
    {
        get => _idade;
        set
        {
            if (value < 0 || value > 120)
                throw new ArgumentOutOfRangeException("Idade inválida.");
            _idade = value;
        }
    }

    // Propriedades somente leitura derivadas
    public bool Aprovado => Media >= 7.0;
    public string Situacao => Aprovado ? "Aprovado" : (Media >= 5 ? "Recuperação" : "Reprovado");

    public Aluno(string nome, int idade)
    {
        Nome = nome;   // usa o setter com validação
        Idade = idade; // usa o setter com validação
    }
}
```

---

## Princípio do Mínimo Privilégio

Regra de ouro: **expor o mínimo necessário**.

```csharp
class Pedido
{
    // ✅ Público: precisa ser acessado de fora
    public int Id { get; }
    public string Cliente { get; }
    public double Total => CalcularTotal();

    // ✅ Private set: pode ser lido, mas só a classe altera
    public StatusPedido Status { get; private set; }
    public DateTime DataCriacao { get; private set; }

    // ✅ Privado: detalhe interno, ninguém precisa saber
    private List<ItemPedido> _itens;
    private double _desconto;

    public Pedido(string cliente)
    {
        Id = GerarId();
        Cliente = cliente;
        Status = StatusPedido.Pendente;
        DataCriacao = DateTime.Now;
        _itens = new List<ItemPedido>();
    }

    // Método público controlado
    public void AdicionarItem(string produto, double preco, int quantidade)
    {
        if (Status != StatusPedido.Pendente)
            throw new InvalidOperationException("Não é possível alterar pedido confirmado.");
        _itens.Add(new ItemPedido(produto, preco, quantidade));
    }

    public void Confirmar()
    {
        if (_itens.Count == 0)
            throw new InvalidOperationException("Pedido vazio não pode ser confirmado.");
        Status = StatusPedido.Confirmado;
    }

    // Método privado — detalhe de implementação
    private double CalcularTotal()
    {
        return _itens.Sum(i => i.Subtotal) * (1 - _desconto);
    }

    private static int _ultimoId = 0;
    private static int GerarId() => ++_ultimoId;
}

enum StatusPedido { Pendente, Confirmado, Enviado, Entregue, Cancelado }

class ItemPedido
{
    public string Produto { get; }
    public double Preco { get; }
    public int Quantidade { get; }
    public double Subtotal => Preco * Quantidade;

    public ItemPedido(string produto, double preco, int quantidade)
    {
        Produto = produto;
        Preco = preco;
        Quantidade = quantidade;
    }
}
```

---

## Resumo

| Conceito | Descrição |
|----------|-----------|
| **Encapsulamento** | Ocultar detalhes internos, expor interface controlada |
| **`private`** | Acessível apenas dentro da classe |
| **`public`** | Acessível por qualquer código |
| **Propriedade** | Acesso controlado aos dados (get/set) |
| **Validação no setter** | Garante que dados inválidos não entrem no objeto |
| **Princípio do mínimo privilégio** | Expor apenas o que é necessário |

---

## Exercícios desta seção

- [Exercício 04 — Encapsulamento com Propriedades](../exercicios/basico/ex04/README.md)
- [Exercício 05 — Construtor com Validação](../exercicios/basico/ex05/README.md)

---

➡️ [Guia 04 — Herança](04-heranca.md)

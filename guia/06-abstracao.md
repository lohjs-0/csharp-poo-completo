# 06 — Abstração

## O que é Abstração?

**Abstração** é o processo de identificar as características essenciais de algo, ignorando os detalhes irrelevantes para o contexto atual.

Quando você usa um carro, você abstrai o funcionamento do motor, da injeção eletrônica, da transmissão — e interage apenas com o volante, pedais e câmbio. Isso é abstração.

---

## Classes Abstratas

Uma **classe abstrata** é uma classe que:
- **Não pode ser instanciada diretamente**
- Define uma estrutura comum para suas subclasses
- Pode ter métodos concretos (com implementação) e abstratos (sem implementação)

```csharp
// ❌ Não faz sentido instanciar "Forma" — ela é abstrata!
// Forma f = new Forma(); // Erro de compilação

// ✅ Só instanciamos os tipos específicos
var c = new Circulo(5);
var r = new Retangulo(4, 6);
```

### Declarando uma Classe Abstrata

```csharp
abstract class Funcionario
{
    // Propriedades comuns (concretas)
    public string Nome { get; }
    public string CPF { get; }
    public DateTime DataAdmissao { get; }

    protected Funcionario(string nome, string cpf)
    {
        Nome = nome;
        CPF = cpf;
        DataAdmissao = DateTime.Now;
    }

    // Método abstrato — OBRIGATÓRIO nas subclasses
    public abstract double CalcularSalario();

    // Método abstrato — OBRIGATÓRIO nas subclasses
    public abstract string TipoCargo();

    // Método concreto — herdado por todas as subclasses
    public int MesesDeEmpresa()
    {
        return (int)((DateTime.Now - DataAdmissao).TotalDays / 30);
    }

    // Método concreto com chamada a método abstrato
    public void ExibirHolerite()
    {
        Console.WriteLine($"╔═══════════════════════════════╗");
        Console.WriteLine($"║          HOLERITE             ║");
        Console.WriteLine($"╠═══════════════════════════════╣");
        Console.WriteLine($"║ Nome: {Nome,-25}║");
        Console.WriteLine($"║ Cargo: {TipoCargo(),-24}║");
        Console.WriteLine($"║ Salário: R${CalcularSalario(),20:F2} ║");
        Console.WriteLine($"╚═══════════════════════════════╝");
    }
}
```

### Implementando as Subclasses

```csharp
class FuncionarioCLT : Funcionario
{
    public double SalarioBase { get; }

    public FuncionarioCLT(string nome, string cpf, double salario)
        : base(nome, cpf)
    {
        SalarioBase = salario;
    }

    public override double CalcularSalario()
    {
        // CLT: salário base + 1/3 de férias proporcionais + 13º proporcional
        double ferias = SalarioBase / 3 / 12;
        double decimoTerceiro = SalarioBase / 12;
        return SalarioBase + ferias + decimoTerceiro;
    }

    public override string TipoCargo() => "CLT";
}

class FuncionarioPJ : Funcionario
{
    public double ValorHora { get; }
    public int HorasTrabalhadas { get; set; }

    public FuncionarioPJ(string nome, string cpf, double valorHora)
        : base(nome, cpf)
    {
        ValorHora = valorHora;
    }

    public override double CalcularSalario()
    {
        return ValorHora * HorasTrabalhadas;
    }

    public override string TipoCargo() => "PJ";
}

class Estagiario : Funcionario
{
    public double BolsaAuxilio { get; }
    public double ValeTransporte { get; }

    public Estagiario(string nome, string cpf, double bolsa, double vt)
        : base(nome, cpf)
    {
        BolsaAuxilio = bolsa;
        ValeTransporte = vt;
    }

    public override double CalcularSalario()
    {
        return BolsaAuxilio + ValeTransporte;
    }

    public override string TipoCargo() => "Estagiário";
}

// Uso:
var clt = new FuncionarioCLT("Ana Silva", "123.456.789-00", 5000);
var pj = new FuncionarioPJ("Carlos Lima", "987.654.321-00", 80);
pj.HorasTrabalhadas = 160;
var est = new Estagiario("Pedro Santos", "111.222.333-44", 1200, 300);

// Lista polimórfica de funcionários!
var funcionarios = new List<Funcionario> { clt, pj, est };

foreach (var f in funcionarios)
{
    f.ExibirHolerite();
    Console.WriteLine();
}

// Folha de pagamento total
double total = funcionarios.Sum(f => f.CalcularSalario());
Console.WriteLine($"Total da folha: R${total:F2}");
```

---

## Método Abstrato vs Método Virtual

| | Abstrato (`abstract`) | Virtual (`virtual`) |
|---|---|---|
| Corpo | Não tem | Tem |
| Subclasse | **Obrigada** a implementar | **Pode** sobrescrever |
| Classe | Deve ser abstrata | Pode ser concreta |

```csharp
abstract class Relatorio
{
    // Abstrato: subclasse DEVE implementar
    public abstract string GerarConteudo();

    // Virtual: subclasse PODE sobrescrever
    public virtual string GerarCabecalho()
    {
        return $"=== Relatório gerado em {DateTime.Now:dd/MM/yyyy HH:mm} ===";
    }

    // Concreto: sempre o mesmo para todos
    public void Imprimir()
    {
        Console.WriteLine(GerarCabecalho());
        Console.WriteLine(GerarConteudo());
        Console.WriteLine("=== FIM ===");
    }
}

class RelatorioVendas : Relatorio
{
    public override string GerarConteudo()
    {
        return "Total de vendas do mês: R$150.000,00\nMaior venda: R$15.000,00";
    }

    // Não é obrigado a sobrescrever GerarCabecalho — usa o padrão
}

class RelatorioEstoque : Relatorio
{
    public override string GerarConteudo()
    {
        return "Itens em estoque: 342\nItens em falta: 12";
    }

    public override string GerarCabecalho() // sobrescreve o padrão
    {
        return $"=== RELATÓRIO DE ESTOQUE — {DateTime.Now:MM/yyyy} ===";
    }
}
```

---

## Template Method Pattern

A abstração é a base do padrão **Template Method** — define o esqueleto de um algoritmo, deixando detalhes para as subclasses:

```csharp
abstract class ProcessadorPedido
{
    // Template Method — define a sequência
    public void ProcessarPedido(string clienteId, double valor)
    {
        Console.WriteLine("Iniciando processamento...");

        ValidarCliente(clienteId);      // passo 1
        ReservarEstoque();              // passo 2
        ProcessarPagamento(valor);      // passo 3
        EnviarConfirmacao(clienteId);   // passo 4

        Console.WriteLine("Pedido processado com sucesso!");
    }

    // Passos abstratos — cada subclasse implementa à sua forma
    protected abstract void ValidarCliente(string clienteId);
    protected abstract void ProcessarPagamento(double valor);

    // Passos concretos — implementação padrão
    protected virtual void ReservarEstoque()
    {
        Console.WriteLine("  → Reservando estoque...");
    }

    protected virtual void EnviarConfirmacao(string clienteId)
    {
        Console.WriteLine($"  → Enviando confirmação para {clienteId}...");
    }
}

class ProcessadorEcommerce : ProcessadorPedido
{
    protected override void ValidarCliente(string clienteId)
    {
        Console.WriteLine($"  → Verificando cadastro online do cliente {clienteId}...");
    }

    protected override void ProcessarPagamento(double valor)
    {
        Console.WriteLine($"  → Processando pagamento de R${valor:F2} via gateway...");
    }

    protected override void EnviarConfirmacao(string clienteId)
    {
        Console.WriteLine($"  → Enviando e-mail + SMS para {clienteId}...");
    }
}

class ProcessadorLoja : ProcessadorPedido
{
    protected override void ValidarCliente(string clienteId)
    {
        Console.WriteLine($"  → Verificando CPF {clienteId} no sistema local...");
    }

    protected override void ProcessarPagamento(double valor)
    {
        Console.WriteLine($"  → Processando no caixa: R${valor:F2}...");
    }
}

// Uso:
var ecommerce = new ProcessadorEcommerce();
ecommerce.ProcessarPedido("cliente@email.com", 299.90);

Console.WriteLine();

var loja = new ProcessadorLoja();
loja.ProcessarPedido("123.456.789-00", 150.00);
```

---

## Quando Usar Classe Abstrata?

✅ Use quando:
- As subclasses **compartilham código comum** (atributos e métodos concretos)
- Você quer **garantir** que certas operações sejam implementadas pelas subclasses
- Existe uma relação **"É UM"** forte entre as classes
- Você precisa de um **estado compartilhado** (campos/propriedades protegidos)

❌ Não use quando:
- A classe não tem nada em comum com suas "subclasses" (use interface)
- Você precisa que uma classe herde de múltiplas fontes (C# não suporta herança múltipla — use interfaces)

---

## Resumo

| Conceito | Descrição |
|----------|-----------|
| `abstract class` | Classe que não pode ser instanciada |
| `abstract método` | Método sem corpo — subclasse obrigada a implementar |
| `virtual método` | Método com corpo — subclasse pode sobrescrever |
| Template Method | Esqueleto do algoritmo na classe base, detalhes nas filhas |

---

## Exercícios desta seção

- [Exercício 03 — Classe Abstrata: Funcionário](../exercicios/intermediario/ex03/README.md)

---

➡️ [Guia 07 — Interfaces](07-interfaces.md)

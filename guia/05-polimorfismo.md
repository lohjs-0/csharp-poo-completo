# 05 — Polimorfismo

## O que é Polimorfismo?

**Polimorfismo** (do grego: "muitas formas") é a capacidade de um objeto se comportar de formas diferentes dependendo do seu tipo real.

Em termos práticos: você pode **tratar objetos diferentes de forma uniforme**, e cada um responde de acordo com sua própria implementação.

---

## Polimorfismo em Ação

```csharp
abstract class Forma
{
    public abstract double CalcularArea();
    public abstract double CalcularPerimetro();

    public void ExibirInfo()
    {
        Console.WriteLine($"Forma: {GetType().Name}");
        Console.WriteLine($"  Área: {CalcularArea():F2}");
        Console.WriteLine($"  Perímetro: {CalcularPerimetro():F2}");
    }
}

class Circulo : Forma
{
    public double Raio { get; }
    public Circulo(double raio) { Raio = raio; }

    public override double CalcularArea() => Math.PI * Raio * Raio;
    public override double CalcularPerimetro() => 2 * Math.PI * Raio;
}

class Retangulo : Forma
{
    public double Largura { get; }
    public double Altura { get; }

    public Retangulo(double largura, double altura)
    {
        Largura = largura;
        Altura = altura;
    }

    public override double CalcularArea() => Largura * Altura;
    public override double CalcularPerimetro() => 2 * (Largura + Altura);
}

class Triangulo : Forma
{
    public double Base { get; }
    public double Altura { get; }
    public double LadoA { get; }
    public double LadoB { get; }
    public double LadoC { get; }

    public Triangulo(double ladoA, double ladoB, double ladoC)
    {
        LadoA = ladoA;
        LadoB = ladoB;
        LadoC = ladoC;
        Base = ladoA;
        // Altura calculada por Heron
        double s = (ladoA + ladoB + ladoC) / 2;
        Altura = 2 * Math.Sqrt(s * (s - ladoA) * (s - ladoB) * (s - ladoC)) / ladoA;
    }

    public override double CalcularArea()
    {
        double s = (LadoA + LadoB + LadoC) / 2;
        return Math.Sqrt(s * (s - LadoA) * (s - LadoB) * (s - LadoC));
    }

    public override double CalcularPerimetro() => LadoA + LadoB + LadoC;
}

// ✨ O poder do polimorfismo: tratar tipos diferentes de forma uniforme
List<Forma> formas = new List<Forma>
{
    new Circulo(5),
    new Retangulo(4, 6),
    new Triangulo(3, 4, 5),
    new Circulo(2),
    new Retangulo(10, 3)
};

// O mesmo código funciona para TODOS os tipos de forma
foreach (var forma in formas)
{
    forma.ExibirInfo(); // cada um responde à sua maneira
    Console.WriteLine();
}

// Calcular área total de todas as formas
double areaTotal = formas.Sum(f => f.CalcularArea());
Console.WriteLine($"Área total: {areaTotal:F2}");
```

---

## Tipos de Polimorfismo em C#

### 1. Polimorfismo de Subtipo (Runtime)
O que vimos acima — a decisão de qual método chamar acontece em tempo de execução:

```csharp
Forma f = new Circulo(3); // variável do tipo Forma, objeto do tipo Circulo
f.CalcularArea();          // chama CalcularArea() do Circulo — decidido em runtime!
```

### 2. Sobrecarga de Método (Compile-time)
Mesmo nome, diferentes parâmetros — a decisão acontece em tempo de compilação:

```csharp
class Impressora
{
    public void Imprimir(string texto)
    {
        Console.WriteLine($"Texto: {texto}");
    }

    public void Imprimir(int numero)
    {
        Console.WriteLine($"Número: {numero}");
    }

    public void Imprimir(double numero, int casasDecimais)
    {
        Console.WriteLine($"Double: {numero.ToString($"F{casasDecimais}")}");
    }

    public void Imprimir(string texto, ConsoleColor cor)
    {
        Console.ForegroundColor = cor;
        Console.WriteLine(texto);
        Console.ResetColor();
    }
}

var imp = new Impressora();
imp.Imprimir("Olá");            // Texto: Olá
imp.Imprimir(42);               // Número: 42
imp.Imprimir(3.14159, 2);       // Double: 3.14
imp.Imprimir("Erro!", ConsoleColor.Red);
```

---

## Polimorfismo com Interfaces

Interfaces potencializam o polimorfismo — veja no [Guia 07](07-interfaces.md) — mas um exemplo básico:

```csharp
interface IDescricao
{
    string ObterDescricao();
}

class Produto : IDescricao
{
    public string Nome { get; set; }
    public double Preco { get; set; }

    public string ObterDescricao()
        => $"Produto: {Nome} - R${Preco:F2}";
}

class Servico : IDescricao
{
    public string Tipo { get; set; }
    public double ValorHora { get; set; }

    public string ObterDescricao()
        => $"Serviço: {Tipo} - R${ValorHora:F2}/hora";
}

// Tratar ambos de forma uniforme
List<IDescricao> itens = new List<IDescricao>
{
    new Produto { Nome = "Notebook", Preco = 3500 },
    new Servico { Tipo = "Consultoria", ValorHora = 150 },
    new Produto { Nome = "Mouse", Preco = 80 }
};

foreach (var item in itens)
    Console.WriteLine(item.ObterDescricao());
```

---

## Exemplo Real: Sistema de Pagamento

```csharp
abstract class MetodoPagamento
{
    public string Descricao { get; }

    protected MetodoPagamento(string descricao)
    {
        Descricao = descricao;
    }

    public abstract bool ProcessarPagamento(double valor);

    public virtual void ExibirRecibo(double valor)
    {
        Console.WriteLine($"=== RECIBO ===");
        Console.WriteLine($"Método: {Descricao}");
        Console.WriteLine($"Valor: R${valor:F2}");
        Console.WriteLine($"Status: Aprovado");
    }
}

class CartaoCredito : MetodoPagamento
{
    public string NumeroCartao { get; }
    public int Parcelas { get; set; }

    public CartaoCredito(string numero, int parcelas = 1)
        : base("Cartão de Crédito")
    {
        NumeroCartao = numero;
        Parcelas = parcelas;
    }

    public override bool ProcessarPagamento(double valor)
    {
        // Simula processamento
        Console.WriteLine($"Processando cartão **** **** **** {NumeroCartao[^4..]}...");
        return true; // aprovado
    }

    public override void ExibirRecibo(double valor)
    {
        base.ExibirRecibo(valor);
        Console.WriteLine($"Parcelas: {Parcelas}x de R${valor / Parcelas:F2}");
    }
}

class Pix : MetodoPagamento
{
    public string ChavePix { get; }

    public Pix(string chave) : base("PIX")
    {
        ChavePix = chave;
    }

    public override bool ProcessarPagamento(double valor)
    {
        Console.WriteLine($"Enviando PIX para {ChavePix}...");
        return true;
    }
}

class Boleto : MetodoPagamento
{
    public DateTime Vencimento { get; }

    public Boleto() : base("Boleto Bancário")
    {
        Vencimento = DateTime.Now.AddDays(3);
    }

    public override bool ProcessarPagamento(double valor)
    {
        Console.WriteLine($"Gerando boleto com vencimento em {Vencimento:dd/MM/yyyy}...");
        return true; // pendente, mas gerado com sucesso
    }

    public override void ExibirRecibo(double valor)
    {
        base.ExibirRecibo(valor);
        Console.WriteLine($"Vencimento: {Vencimento:dd/MM/yyyy}");
        Console.WriteLine("Status: Aguardando pagamento");
    }
}

// O polimorfismo brilha aqui — a função não precisa saber o tipo exato!
static void RealizarPagamento(MetodoPagamento metodo, double valor)
{
    Console.WriteLine($"\nIniciando pagamento de R${valor:F2}...");

    if (metodo.ProcessarPagamento(valor))
    {
        metodo.ExibirRecibo(valor);
    }
    else
    {
        Console.WriteLine("Pagamento recusado!");
    }
}

// Uso:
RealizarPagamento(new CartaoCredito("1234567890123456", 3), 900);
RealizarPagamento(new Pix("cpf@banco.com"), 150);
RealizarPagamento(new Boleto(), 75.50);
```

---

## Covariância e Contravariância (Avançado)

```csharp
// Covariância: retornar tipo mais específico
class Animal { }
class Cachorro : Animal { }

class FabricaAnimal
{
    public virtual Animal Criar() => new Animal();
}

class FabricaCachorro : FabricaAnimal
{
    // Retorna Cachorro (mais específico que Animal) — covariância
    public override Cachorro Criar() => new Cachorro();
}
```

---

## Resumo

| Tipo | Como funciona | Quando decide |
|------|---------------|---------------|
| Subtipo (override) | Classe filha sobrescreve virtual | Runtime |
| Sobrecarga | Mesmo nome, parâmetros diferentes | Compile-time |
| Interface | Diferentes classes implementam mesma interface | Runtime |

**Benefícios do polimorfismo:**
- Código genérico que funciona com novos tipos sem modificação
- Fácil de estender (princípio Aberto/Fechado)
- Código mais limpo e expressivo

---

## Exercícios desta seção

- [Exercício 02 — Polimorfismo com Formas Geométricas](../exercicios/intermediario/ex02/README.md)
- [Exercício 07 — Polimorfismo com Notificações](../exercicios/intermediario/ex07/README.md)

---

➡️ [Guia 06 — Abstração](06-abstracao.md)

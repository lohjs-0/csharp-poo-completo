# 04 — Herança

## O que é Herança?

**Herança** é um mecanismo que permite criar uma nova classe baseada em uma existente. A nova classe (**filha/derivada**) herda todos os membros da classe **pai/base** e pode:
- Usar os membros herdados como estão
- **Sobrescrever** comportamentos (override)
- **Adicionar** novos membros

---

## Analogia do Mundo Real

```
Animal
├── tem: Nome, Idade
├── pode: Comer(), Dormir(), FazerSom()
│
├── Cachorro (herda de Animal)
│   ├── tem tudo de Animal +
│   ├── tem: Raca
│   └── pode: Latir(), Buscar()
│
└── Gato (herda de Animal)
    ├── tem tudo de Animal +
    ├── tem: EhCastrado
    └── pode: Miar(), Arranhar()
```

---

## Sintaxe da Herança em C#

Use `: NomeDaClasseBase` para herdar:

```csharp
// Classe base (pai)
class Animal
{
    public string Nome { get; set; }
    public int Idade { get; set; }

    public Animal(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
    }

    public void Comer()
    {
        Console.WriteLine($"{Nome} está comendo...");
    }

    public void Dormir()
    {
        Console.WriteLine($"{Nome} está dormindo... zzz");
    }

    // virtual = pode ser sobrescrito por classes filhas
    public virtual void FazerSom()
    {
        Console.WriteLine($"{Nome} faz algum som...");
    }

    public override string ToString()
    {
        return $"{GetType().Name}: {Nome} ({Idade} anos)";
    }
}

// Classe derivada (filha)
class Cachorro : Animal
{
    public string Raca { get; set; }

    // "base(...)" chama o construtor do pai
    public Cachorro(string nome, int idade, string raca)
        : base(nome, idade)
    {
        Raca = raca;
    }

    // override = sobrescreve o método virtual do pai
    public override void FazerSom()
    {
        Console.WriteLine($"{Nome} diz: Au au!");
    }

    // Método próprio da subclasse
    public void Buscar()
    {
        Console.WriteLine($"{Nome} buscou o objeto!");
    }
}

class Gato : Animal
{
    public bool EhCastrado { get; set; }

    public Gato(string nome, int idade, bool ehCastrado)
        : base(nome, idade)
    {
        EhCastrado = ehCastrado;
    }

    public override void FazerSom()
    {
        Console.WriteLine($"{Nome} diz: Miau!");
    }

    public void Arranhar()
    {
        Console.WriteLine($"{Nome} arranhou o sofá!");
    }
}
```

---

## Usando as Classes

```csharp
var rex = new Cachorro("Rex", 3, "Pastor Alemão");
var mingau = new Gato("Mingau", 5, true);

// Métodos herdados do pai
rex.Comer();    // Rex está comendo...
mingau.Dormir(); // Mingau está dormindo... zzz

// Métodos sobrescritos
rex.FazerSom();    // Rex diz: Au au!
mingau.FazerSom(); // Mingau diz: Miau!

// Métodos próprios
rex.Buscar();      // Rex buscou o objeto!
mingau.Arranhar(); // Mingau arranhou o sofá!

// ToString sobrescrito
Console.WriteLine(rex);    // Cachorro: Rex (3 anos)
Console.WriteLine(mingau); // Gato: Mingau (5 anos)
```

---

## A palavra-chave `base`

Use `base` para acessar membros da classe pai:

```csharp
class Funcionario
{
    public string Nome { get; }
    public double SalarioBase { get; protected set; }

    public Funcionario(string nome, double salarioBase)
    {
        Nome = nome;
        SalarioBase = salarioBase;
    }

    public virtual double CalcularSalario()
    {
        return SalarioBase;
    }

    public virtual void ExibirInfo()
    {
        Console.WriteLine($"Funcionário: {Nome}");
        Console.WriteLine($"Salário: R${CalcularSalario():F2}");
    }
}

class Gerente : Funcionario
{
    public double Bonus { get; set; }

    public Gerente(string nome, double salarioBase, double bonus)
        : base(nome, salarioBase) // chama construtor do pai
    {
        Bonus = bonus;
    }

    public override double CalcularSalario()
    {
        // base.CalcularSalario() chama o método do pai
        return base.CalcularSalario() + Bonus;
    }

    public override void ExibirInfo()
    {
        base.ExibirInfo(); // chama o método do pai primeiro
        Console.WriteLine($"Bônus: R${Bonus:F2}");
        Console.WriteLine($"Cargo: Gerente");
    }
}

class Vendedor : Funcionario
{
    public double ComissaoPorVenda { get; set; }
    public int TotalVendas { get; set; }

    public Vendedor(string nome, double salarioBase, double comissao)
        : base(nome, salarioBase)
    {
        ComissaoPorVenda = comissao;
    }

    public override double CalcularSalario()
    {
        return base.CalcularSalario() + (ComissaoPorVenda * TotalVendas);
    }
}

// Uso:
var func = new Funcionario("João", 3000);
var gerente = new Gerente("Maria", 5000, 2000);
var vendedor = new Vendedor("Carlos", 2000, 50);
vendedor.TotalVendas = 30;

func.ExibirInfo();
Console.WriteLine("---");
gerente.ExibirInfo();
Console.WriteLine("---");
Console.WriteLine($"{vendedor.Nome}: R${vendedor.CalcularSalario():F2}"); // 2000 + 50*30 = 3500
```

---

## Herança em Cadeia

```csharp
class Veiculo
{
    public string Marca { get; }
    public int Ano { get; }

    public Veiculo(string marca, int ano)
    {
        Marca = marca;
        Ano = ano;
    }

    public virtual string Descricao() => $"{Marca} ({Ano})";
}

class Carro : Veiculo
{
    public int NumeroPortas { get; }

    public Carro(string marca, int ano, int portas)
        : base(marca, ano)
    {
        NumeroPortas = portas;
    }

    public override string Descricao() => $"{base.Descricao()} - {NumeroPortas} portas";
}

class CarroEsportivo : Carro // herda de Carro, que herda de Veiculo
{
    public double VelocidadeMaxima { get; }

    public CarroEsportivo(string marca, int ano, double velMax)
        : base(marca, ano, 2) // esportivos têm 2 portas
    {
        VelocidadeMaxima = velMax;
    }

    public override string Descricao()
        => $"{base.Descricao()} - {VelocidadeMaxima} km/h (max)";
}

var esportivo = new CarroEsportivo("Ferrari", 2023, 320);
Console.WriteLine(esportivo.Descricao());
// Ferrari (2023) - 2 portas - 320 km/h (max)
```

---

## `sealed` — Impedindo Herança

Use `sealed` quando não quiser que uma classe seja herdada:

```csharp
sealed class ChaveDeSeguranca
{
    // Esta classe não pode ser herdada
    private readonly string _chave;

    public ChaveDeSeguranca(string chave)
    {
        _chave = chave;
    }

    public bool Validar(string tentativa) => tentativa == _chave;
}

// class ChaveExtendida : ChaveDeSeguranca { } // ERRO! sealed não pode ser herdada
```

---

## Verificando Tipos: `is` e `as`

```csharp
Animal animal = new Cachorro("Rex", 3, "Labrador");

// "is" verifica o tipo
if (animal is Cachorro)
{
    Console.WriteLine("É um cachorro!");
}

// Pattern matching com "is"
if (animal is Cachorro cachorro)
{
    cachorro.Buscar(); // já tem o tipo correto!
}

// "as" tenta converter (retorna null se falhar)
Cachorro? c = animal as Cachorro;
if (c != null)
{
    c.Buscar();
}

// Switch com pattern matching
void ProcessarAnimal(Animal a)
{
    switch (a)
    {
        case Cachorro dog:
            Console.WriteLine($"{dog.Nome} é um cachorro da raça {dog.Raca}");
            break;
        case Gato cat:
            Console.WriteLine($"{cat.Nome} é um gato");
            break;
        default:
            Console.WriteLine($"{a.Nome} é um animal desconhecido");
            break;
    }
}
```

---

## Boas Práticas de Herança

✅ **Use herança quando existir relação "É UM":**
- `Cachorro` **é um** `Animal` ✅
- `Gerente` **é um** `Funcionario` ✅

❌ **Não use herança para relação "TEM UM":**
- `Carro` **tem um** `Motor` — use composição, não herança ❌

✅ **Prefira composição sobre herança quando em dúvida**

```csharp
// ❌ Herança incorreta
class CarroComMotor : Motor { } // Carro não É um Motor!

// ✅ Composição correta
class Carro
{
    private Motor _motor; // Carro TEM um Motor
    public Carro() { _motor = new Motor(); }
}
```

---

## Resumo

| Conceito | Descrição |
|----------|-----------|
| `: ClasseBase` | Herda de uma classe |
| `base(...)` | Chama construtor/método da classe pai |
| `virtual` | Método que pode ser sobrescrito |
| `override` | Sobrescreve um método virtual |
| `sealed` | Impede herança da classe ou sobrescrita do método |
| `is` / `as` | Verifica/converte tipos em tempo de execução |

---

## Exercícios desta seção

- [Exercício 01 — Herança: Animal](../exercicios/intermediario/ex01/README.md)
- [Exercício 05 — Herança em Cadeia: Veículo](../exercicios/intermediario/ex05/README.md)

---

➡️ [Guia 05 — Polimorfismo](05-polimorfismo.md)

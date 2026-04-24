# 02 — Classes e Objetos

## Declarando uma Classe

A sintaxe básica de uma classe em C#:

```csharp
class NomeDaClasse
{
    // Campos (dados)
    // Propriedades
    // Construtores
    // Métodos
}
```

Vamos criar uma classe `Pessoa` do zero:

```csharp
class Pessoa
{
    // Propriedades (dados da classe)
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Email { get; set; }

    // Construtor padrão
    public Pessoa()
    {
        Nome = "Sem nome";
        Idade = 0;
    }

    // Construtor com parâmetros
    public Pessoa(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
    }

    // Método
    public void Apresentar()
    {
        Console.WriteLine($"Olá! Meu nome é {Nome} e tenho {Idade} anos.");
    }

    // Método que retorna valor
    public bool EhMaiorDeIdade()
    {
        return Idade >= 18;
    }
}
```

---

## Criando Objetos (Instâncias)

```csharp
// Usando construtor padrão
Pessoa p1 = new Pessoa();
p1.Nome = "Ana";
p1.Idade = 25;

// Usando construtor com parâmetros
Pessoa p2 = new Pessoa("Carlos", 30);
p2.Email = "carlos@email.com";

// Usando var (inferência de tipo)
var p3 = new Pessoa("Maria", 17);

// Inicializador de objeto (sintaxe alternativa)
var p4 = new Pessoa
{
    Nome = "João",
    Idade = 22,
    Email = "joao@email.com"
};

// Chamando métodos
p1.Apresentar(); // Olá! Meu nome é Ana e tenho 25 anos.
Console.WriteLine(p3.EhMaiorDeIdade()); // False
Console.WriteLine(p2.EhMaiorDeIdade()); // True
```

---

## Campos vs Propriedades

Em C#, usamos **propriedades** (não campos públicos) para expor dados:

```csharp
class Produto
{
    // ❌ Evite — campo público (sem controle)
    public double preco;

    // ✅ Prefira — propriedade (com getter/setter)
    public double Preco { get; set; }

    // ✅ Melhor ainda — com validação no setter
    private double _estoque;
    public double Estoque
    {
        get { return _estoque; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Estoque não pode ser negativo");
            _estoque = value;
        }
    }

    // Propriedade somente leitura (computed)
    public bool EmEstoque => Estoque > 0;
}
```

---

## Construtores

O **construtor** é um método especial chamado quando o objeto é criado:

```csharp
class ContaBancaria
{
    public string Titular { get; }
    public string Numero { get; }
    public double Saldo { get; private set; }

    // Construtor obrigatório (requer dados essenciais)
    public ContaBancaria(string titular, string numero)
    {
        Titular = titular;
        Numero = numero;
        Saldo = 0;
    }

    // Sobrecarga de construtor
    public ContaBancaria(string titular, string numero, double saldoInicial)
        : this(titular, numero) // chama o construtor acima
    {
        Saldo = saldoInicial;
    }

    public void Depositar(double valor)
    {
        Saldo += valor;
        Console.WriteLine($"Depósito de R${valor:F2}. Saldo: R${Saldo:F2}");
    }
}

// Uso:
var conta = new ContaBancaria("Ana", "001-1", 500);
conta.Depositar(200); // Depósito de R$200,00. Saldo: R$700,00
```

---

## Métodos

Métodos definem o **comportamento** da classe:

```csharp
class Calculadora
{
    // Método que retorna valor
    public double Somar(double a, double b)
    {
        return a + b;
    }

    // Método sem retorno (void)
    public void ExibirResultado(double resultado)
    {
        Console.WriteLine($"Resultado: {resultado}");
    }

    // Método estático (não precisa de instância)
    public static double Quadrado(double numero)
    {
        return numero * numero;
    }

    // Método com parâmetros opcionais
    public double Potencia(double base_, double expoente = 2)
    {
        return Math.Pow(base_, expoente);
    }
}

// Uso:
var calc = new Calculadora();
double resultado = calc.Somar(3, 4);       // 7
calc.ExibirResultado(resultado);            // Resultado: 7
Console.WriteLine(Calculadora.Quadrado(5)); // 25 (método estático)
Console.WriteLine(calc.Potencia(3));        // 9 (usa expoente padrão = 2)
Console.WriteLine(calc.Potencia(2, 10));    // 1024
```

---

## A palavra-chave `this`

`this` se refere à instância atual do objeto:

```csharp
class Circulo
{
    public double Raio { get; set; }

    // Quando o parâmetro tem o mesmo nome que a propriedade,
    // usamos "this" para diferenciar
    public Circulo(double raio)
    {
        this.Raio = raio; // this.Raio = propriedade, raio = parâmetro
    }

    public double CalcularArea()
    {
        return Math.PI * Raio * Raio;
    }

    // Retornando "this" permite encadeamento de métodos
    public Circulo SetRaio(double raio)
    {
        this.Raio = raio;
        return this;
    }
}
```

---

## Membros Estáticos

Membros `static` pertencem à **classe**, não às instâncias:

```csharp
class Contador
{
    // Campo estático: compartilhado por TODAS as instâncias
    private static int _totalInstancias = 0;

    public int Id { get; }
    public string Nome { get; set; }

    public Contador(string nome)
    {
        _totalInstancias++;
        Id = _totalInstancias;
        Nome = nome;
    }

    // Propriedade estática
    public static int TotalInstancias => _totalInstancias;

    // Método estático
    public static void ResetarContador()
    {
        _totalInstancias = 0;
    }
}

// Uso:
var c1 = new Contador("Primeiro");
var c2 = new Contador("Segundo");
var c3 = new Contador("Terceiro");

Console.WriteLine(Contador.TotalInstancias); // 3
Console.WriteLine(c1.Id);                    // 1
Console.WriteLine(c2.Id);                    // 2
```

---

## Exemplo Completo: Classe Aluno

```csharp
class Aluno
{
    // Propriedades
    public string Nome { get; }
    public int Matricula { get; }
    private List<double> _notas;

    // Contador estático
    private static int _proximaMatricula = 1000;

    // Construtor
    public Aluno(string nome)
    {
        Nome = nome;
        Matricula = _proximaMatricula++;
        _notas = new List<double>();
    }

    // Métodos
    public void AdicionarNota(double nota)
    {
        if (nota < 0 || nota > 10)
            throw new ArgumentException("Nota deve ser entre 0 e 10");
        _notas.Add(nota);
    }

    public double CalcularMedia()
    {
        if (_notas.Count == 0) return 0;
        return _notas.Sum() / _notas.Count;
    }

    public string ObterSituacao()
    {
        double media = CalcularMedia();
        if (media >= 7) return "Aprovado";
        if (media >= 5) return "Recuperação";
        return "Reprovado";
    }

    public void ExibirBoletim()
    {
        Console.WriteLine($"=== Boletim de {Nome} (Mat: {Matricula}) ===");
        for (int i = 0; i < _notas.Count; i++)
            Console.WriteLine($"  Nota {i + 1}: {_notas[i]:F1}");
        Console.WriteLine($"  Média: {CalcularMedia():F2}");
        Console.WriteLine($"  Situação: {ObterSituacao()}");
    }
}

// Programa principal
var aluno1 = new Aluno("Ana Silva");
aluno1.AdicionarNota(8.5);
aluno1.AdicionarNota(7.0);
aluno1.AdicionarNota(9.0);
aluno1.ExibirBoletim();

var aluno2 = new Aluno("Pedro Santos");
aluno2.AdicionarNota(4.0);
aluno2.AdicionarNota(5.5);
aluno2.ExibirBoletim();
```

**Saída:**
```
=== Boletim de Ana Silva (Mat: 1000) ===
  Nota 1: 8.5
  Nota 2: 7.0
  Nota 3: 9.0
  Média: 8.17
  Situação: Aprovado

=== Boletim de Pedro Santos (Mat: 1001) ===
  Nota 1: 4.0
  Nota 2: 5.5
  Média: 4.75
  Situação: Reprovado
```

---

## Resumo

| Conceito | O que é |
|----------|---------|
| **Classe** | Molde / modelo com dados e comportamentos |
| **Objeto** | Instância concreta de uma classe |
| **Propriedade** | Dado que um objeto possui |
| **Método** | Comportamento / ação que um objeto executa |
| **Construtor** | Método especial que inicializa o objeto |
| **Estático** | Pertence à classe, não à instância |

---

## Exercícios desta seção

- [Exercício 01 — Criar Classe Pessoa](../exercicios/basico/ex01/README.md)
- [Exercício 02 — Classe Retângulo](../exercicios/basico/ex02/README.md)
- [Exercício 03 — Conta Bancária Simples](../exercicios/basico/ex03/README.md)

---

➡️ [Guia 03 — Encapsulamento](03-encapsulamento.md)

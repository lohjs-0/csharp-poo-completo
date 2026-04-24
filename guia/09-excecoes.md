# 09 — Tratamento de Exceções

## O que são Exceções?

**Exceções** são eventos que ocorrem durante a execução do programa e interrompem o fluxo normal. Em POO, exceções são **objetos** que carregam informações sobre o erro.

```
System.Exception (raiz de todas as exceções)
├── SystemException
│   ├── ArgumentException
│   │   └── ArgumentNullException
│   │   └── ArgumentOutOfRangeException
│   ├── InvalidOperationException
│   ├── NullReferenceException
│   ├── IndexOutOfRangeException
│   └── DivideByZeroException
└── ApplicationException (base para exceções de negócio)
```

---

## try / catch / finally

```csharp
try
{
    // Código que pode lançar exceção
    int[] numeros = { 1, 2, 3 };
    Console.WriteLine(numeros[10]); // IndexOutOfRangeException!
}
catch (IndexOutOfRangeException ex)
{
    // Tratamento específico
    Console.WriteLine($"Índice inválido: {ex.Message}");
}
catch (Exception ex)
{
    // Tratamento genérico (captura qualquer exceção)
    Console.WriteLine($"Erro inesperado: {ex.Message}");
}
finally
{
    // SEMPRE executado, com ou sem exceção
    Console.WriteLine("Bloco finally executado.");
}
```

---

## Múltiplos catch

```csharp
static double Dividir(double a, double b)
{
    if (b == 0)
        throw new DivideByZeroException("Divisão por zero não é permitida.");
    return a / b;
}

static void ProcessarEntrada(string input)
{
    try
    {
        double numero = double.Parse(input);
        double resultado = Dividir(100, numero);
        Console.WriteLine($"Resultado: {resultado}");
    }
    catch (FormatException)
    {
        Console.WriteLine($"'{input}' não é um número válido.");
    }
    catch (DivideByZeroException ex)
    {
        Console.WriteLine($"Erro matemático: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro inesperado: {ex.GetType().Name} — {ex.Message}");
    }
}

ProcessarEntrada("25");  // Resultado: 4
ProcessarEntrada("abc"); // 'abc' não é um número válido.
ProcessarEntrada("0");   // Erro matemático: Divisão por zero não é permitida.
```

---

## Criando Exceções Customizadas

Crie suas próprias exceções para erros de negócio:

```csharp
// Base para exceções de negócio
class NegocioException : Exception
{
    public string Codigo { get; }

    public NegocioException(string codigo, string mensagem)
        : base(mensagem)
    {
        Codigo = codigo;
    }

    public NegocioException(string codigo, string mensagem, Exception inner)
        : base(mensagem, inner)
    {
        Codigo = codigo;
    }
}

// Exceções específicas do domínio
class SaldoInsuficienteException : NegocioException
{
    public double SaldoAtual { get; }
    public double ValorRequerido { get; }

    public SaldoInsuficienteException(double saldoAtual, double valorRequerido)
        : base("SALDO_INSUFICIENTE",
               $"Saldo insuficiente. Atual: R${saldoAtual:F2}, Necessário: R${valorRequerido:F2}")
    {
        SaldoAtual = saldoAtual;
        ValorRequerido = valorRequerido;
    }
}

class ContaInativaException : NegocioException
{
    public string NumeroConta { get; }

    public ContaInativaException(string numero)
        : base("CONTA_INATIVA", $"A conta {numero} está inativa.")
    {
        NumeroConta = numero;
    }
}

class LimiteTransferenciaException : NegocioException
{
    public LimiteTransferenciaException(double limite)
        : base("LIMITE_EXCEDIDO", $"Valor excede o limite de transferência de R${limite:F2}.")
    {
    }
}
```

---

## Usando Exceções Customizadas

```csharp
class ContaBancaria
{
    public string Numero { get; }
    public string Titular { get; }
    public double Saldo { get; private set; }
    public bool Ativa { get; private set; }

    private const double LimiteTransferencia = 10000;

    public ContaBancaria(string numero, string titular, double saldoInicial = 0)
    {
        Numero = numero;
        Titular = titular;
        Saldo = saldoInicial;
        Ativa = true;
    }

    public void Sacar(double valor)
    {
        if (!Ativa)
            throw new ContaInativaException(Numero);

        if (valor <= 0)
            throw new ArgumentException("Valor de saque deve ser positivo.", nameof(valor));

        if (valor > Saldo)
            throw new SaldoInsuficienteException(Saldo, valor);

        Saldo -= valor;
    }

    public void Transferir(ContaBancaria destino, double valor)
    {
        if (!Ativa)
            throw new ContaInativaException(Numero);

        if (!destino.Ativa)
            throw new ContaInativaException(destino.Numero);

        if (valor > LimiteTransferencia)
            throw new LimiteTransferenciaException(LimiteTransferencia);

        if (valor > Saldo)
            throw new SaldoInsuficienteException(Saldo, valor);

        Saldo -= valor;
        destino.Saldo += valor;
    }

    public void Desativar() => Ativa = false;
}

// Tratamento inteligente das exceções de negócio:
static void RealizarOperacao(Action operacao)
{
    try
    {
        operacao();
        Console.WriteLine("  ✓ Operação realizada com sucesso.");
    }
    catch (SaldoInsuficienteException ex)
    {
        Console.WriteLine($"  ✗ [{ex.Codigo}] {ex.Message}");
        Console.WriteLine($"    → Saldo atual: R${ex.SaldoAtual:F2}");
    }
    catch (ContaInativaException ex)
    {
        Console.WriteLine($"  ✗ [{ex.Codigo}] {ex.Message}");
    }
    catch (LimiteTransferenciaException ex)
    {
        Console.WriteLine($"  ✗ [{ex.Codigo}] {ex.Message}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"  ✗ Argumento inválido: {ex.Message}");
    }
}

var contaA = new ContaBancaria("001", "Ana", 500);
var contaB = new ContaBancaria("002", "Carlos", 200);

Console.WriteLine("=== Operações Bancárias ===");

RealizarOperacao(() => contaA.Sacar(200));
RealizarOperacao(() => contaA.Sacar(400));   // saldo insuficiente
RealizarOperacao(() => contaA.Transferir(contaB, 200));

contaB.Desativar();
RealizarOperacao(() => contaA.Transferir(contaB, 50)); // conta inativa
```

---

## Relançando Exceções

```csharp
static void ProcessarArquivo(string caminho)
{
    try
    {
        // Lê o arquivo...
        var conteudo = File.ReadAllText(caminho);
        // processa...
    }
    catch (FileNotFoundException ex)
    {
        // Adiciona contexto e relança
        throw new NegocioException("ARQUIVO_NAO_ENCONTRADO",
            $"Arquivo de configuração não encontrado: {caminho}", ex);
    }
    catch (Exception ex)
    {
        // "throw" sem argumento preserva o stack trace original
        Console.WriteLine($"Log: {ex.Message}");
        throw; // ← relança a mesma exceção sem perder o stack trace
    }
}
```

---

## Padrão: Validação sem Exceções (Try-Parse)

Para casos onde o "erro" é esperado, use o padrão Try-Parse:

```csharp
class Conversor
{
    // ❌ Usa exceção para fluxo normal — ruim!
    public static int ConverterParaInt(string texto)
    {
        return int.Parse(texto); // lança se falhar
    }

    // ✅ Retorna bool + out param — melhor para casos esperados
    public static bool TentarConverterParaInt(string texto, out int resultado)
    {
        return int.TryParse(texto, out resultado);
    }
}

// Uso:
string entrada = Console.ReadLine() ?? "";

if (int.TryParse(entrada, out int numero))
{
    Console.WriteLine($"Número válido: {numero}");
}
else
{
    Console.WriteLine("Por favor, insira um número válido.");
    // SEM exceção! Isso é o comportamento esperado.
}
```

---

## Boas Práticas

```csharp
// ✅ Capture exceções específicas, não genéricas
catch (FileNotFoundException ex) { /* específico */ }
// catch (Exception ex) { /* muito genérico — evite */ }

// ✅ Não engula exceções silenciosamente
// ❌ catch (Exception) { } // BAD — esconde erros!
// ✅ catch (Exception ex) { Logger.Log(ex); throw; }

// ✅ Use exceções para condições EXCEPCIONAIS, não para fluxo normal
// ❌ Usar exceção para verificar se usuário existe
// ✅ Usar retorno (bool, null, Result<T>) para verificações de negócio esperadas

// ✅ Inclua informações úteis na mensagem
throw new ArgumentException($"Valor '{valor}' inválido para o parâmetro '{nameof(preco)}'. Deve ser positivo.");

// ✅ Limpe recursos no finally
FileStream? arquivo = null;
try
{
    arquivo = File.OpenRead("dados.txt");
    // processa...
}
finally
{
    arquivo?.Close(); // sempre fecha, mesmo com exceção
}

// ✅ Melhor ainda: using statement
using var arquivo2 = File.OpenRead("dados.txt"); // fecha automaticamente
// processa...
```

---

## Resumo

| Conceito | Descrição |
|----------|-----------|
| `try` | Envolve código que pode lançar exceção |
| `catch` | Captura e trata a exceção |
| `finally` | Executado sempre (com ou sem exceção) |
| `throw` | Lança uma exceção |
| `throw;` | Relança a exceção atual sem perder stack trace |
| Exceção customizada | Herda de `Exception` ou `ApplicationException` |

---

## Exercícios desta seção

- [Exercício 09 — Exceções Customizadas](../exercicios/intermediario/ex09/README.md)

---

➡️ [Guia 10 — Boas Práticas](10-boas-praticas.md)

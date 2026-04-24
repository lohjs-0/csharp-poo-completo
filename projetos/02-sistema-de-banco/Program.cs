using Banco.Models;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("╔══════════════════════════════╗");
Console.WriteLine("║   Sistema Bancário Digital   ║");
Console.WriteLine("╚══════════════════════════════╝\n");

// Criar contas
Console.WriteLine("=== Abrindo Contas ===");
var corrente = new ContaCorrente("Ana Silva", saldoInicial: 1000, limite: 500);
var poupanca = new ContaPoupanca("Ana Silva", saldoInicial: 5000, taxaMensal: 0.006);
var investimento = new ContaInvestimento("Carlos Lima", saldoInicial: 10000, taxaAnual: 0.14);
var correnteCarlos = new ContaCorrente("Carlos Lima", saldoInicial: 2000);

Console.WriteLine($"  Conta corrente:    {corrente.Numero} | Saldo: R${corrente.Saldo:F2}");
Console.WriteLine($"  Conta poupança:    {poupanca.Numero} | Saldo: R${poupanca.Saldo:F2}");
Console.WriteLine($"  Conta investimento:{investimento.Numero} | Saldo: R${investimento.Saldo:F2}");

// Operações na conta corrente
Console.WriteLine("\n=== Operações — Conta Corrente Ana ===");
corrente.Depositar(500);
corrente.Sacar(200);
corrente.Transferir(correnteCarlos, 300);

// Usar o cheque especial
Console.WriteLine("\n=== Usando Cheque Especial ===");
Console.WriteLine($"Saldo: R${corrente.Saldo:F2} | Limite: R${corrente.LimiteCredito:F2}");
Console.WriteLine($"Disponível: R${corrente.SaldoDisponivel:F2}");
corrente.Sacar(1200); // vai usar o cheque especial
Console.WriteLine($"Novo saldo: R${corrente.Saldo:F2}");

// Tentar sacar além do limite
Console.WriteLine("\n=== Tentando sacar além do limite ===");
try { corrente.Sacar(1000); }
catch (SaldoInsuficienteException ex) { Console.WriteLine($"✗ {ex.Message}"); }

// Rendimento na poupança
Console.WriteLine("\n=== Rendimento da Poupança (3 meses) ===");
Console.WriteLine($"Saldo inicial: R${poupanca.Saldo:F2}");
for (int i = 1; i <= 3; i++)
{
    Console.Write($"Mês {i}: ");
    poupanca.AplicarRendimento();
}
Console.WriteLine($"Saldo após 3 meses: R${poupanca.Saldo:F2}");

// Rendimento no investimento
Console.WriteLine("\n=== Rendimento do Investimento (6 meses) ===");
Console.WriteLine($"Saldo inicial: R${investimento.Saldo:F2} | Taxa anual: 14%");
for (int i = 1; i <= 6; i++)
{
    Console.Write($"Mês {i}: ");
    investimento.AplicarRendimento();
}
Console.WriteLine($"Saldo após 6 meses: R${investimento.Saldo:F2}");

// Polimorfismo — lista de contas
Console.WriteLine("\n=== Relatório de Todas as Contas ===");
var todasContas = new List<Conta> { corrente, poupanca, investimento, correnteCarlos };
foreach (var conta in todasContas)
    Console.WriteLine($"  [{conta.TipoConta,-20}] {conta.Numero} — {conta.Titular}: R${conta.Saldo:F2}");

// Extratos
corrente.ExibirExtrato();
poupanca.ExibirExtrato();

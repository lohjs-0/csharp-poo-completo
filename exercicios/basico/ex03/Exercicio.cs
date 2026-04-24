// Exercício 03 — Conta Bancária Simples

class ContaBancaria
{
    // TODO: Propriedades (Titular, Numero, Saldo)
    // TODO: Campo privado para histórico de transações

    // TODO: Construtor(titular, numero, saldoInicial = 0)

    // TODO: Depositar(double valor)
    // Valida que valor > 0
    // Adiciona ao histórico

    // TODO: Sacar(double valor) → bool
    // Retorna false se saldo insuficiente

    // TODO: TransferirPara(ContaBancaria destino, double valor) → bool

    // TODO: ExibirExtrato()
    // Mostre todas as transações e o saldo atual
}

class Programa
{
    static void Main()
    {
        var contaA = new ContaBancaria("Ana", "001-1", 500);
        var contaB = new ContaBancaria("Carlos", "002-2");

        contaA.Depositar(300);
        contaA.Sacar(100);
        contaA.TransferirPara(contaB, 250);

        Console.WriteLine("=== Extrato de Ana ===");
        contaA.ExibirExtrato();

        Console.WriteLine("\n=== Extrato de Carlos ===");
        contaB.ExibirExtrato();
    }
}

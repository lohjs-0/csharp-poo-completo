// Exercício 03 — SOLUÇÃO

class ContaBancaria
{
    private double _saldo;
    private readonly List<string> _historico = new();

    public string Titular { get; }
    public string Numero { get; }
    public double Saldo => _saldo;

    public ContaBancaria(string titular, string numero, double saldoInicial = 0)
    {
        Titular = titular;
        Numero = numero;
        _saldo = saldoInicial;
        if (saldoInicial > 0)
            _historico.Add($"[{DateTime.Now:HH:mm}] Saldo inicial: +R${saldoInicial:F2}");
    }

    public void Depositar(double valor)
    {
        if (valor <= 0) throw new ArgumentException("Valor de depósito deve ser positivo.");
        _saldo += valor;
        _historico.Add($"[{DateTime.Now:HH:mm}] Depósito: +R${valor:F2}");
        Console.WriteLine($"Depósito de R${valor:F2} realizado. Saldo: R${_saldo:F2}");
    }

    public bool Sacar(double valor)
    {
        if (valor <= 0) { Console.WriteLine("Valor inválido."); return false; }
        if (valor > _saldo) { Console.WriteLine("Saldo insuficiente."); return false; }
        _saldo -= valor;
        _historico.Add($"[{DateTime.Now:HH:mm}] Saque: -R${valor:F2}");
        Console.WriteLine($"Saque de R${valor:F2} realizado. Saldo: R${_saldo:F2}");
        return true;
    }

    public bool TransferirPara(ContaBancaria destino, double valor)
    {
        if (!Sacar(valor)) return false;
        destino._saldo += valor;
        destino._historico.Add($"[{DateTime.Now:HH:mm}] Transferência recebida de {Titular}: +R${valor:F2}");
        _historico[^1] = $"[{DateTime.Now:HH:mm}] Transferência para {destino.Titular}: -R${valor:F2}";
        return true;
    }

    public void ExibirExtrato()
    {
        Console.WriteLine($"Conta: {Numero} | Titular: {Titular}");
        foreach (var item in _historico) Console.WriteLine($"  {item}");
        Console.WriteLine($"  Saldo atual: R${_saldo:F2}");
    }
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
        Console.WriteLine("\n=== Extrato de Ana ===");
        contaA.ExibirExtrato();
        Console.WriteLine("\n=== Extrato de Carlos ===");
        contaB.ExibirExtrato();
    }
}

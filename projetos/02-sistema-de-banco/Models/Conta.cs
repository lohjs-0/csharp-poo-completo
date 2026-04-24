namespace Banco.Models;

// Exceções do domínio
public class SaldoInsuficienteException : Exception
{
    public double SaldoAtual { get; }
    public double ValorRequerido { get; }
    public SaldoInsuficienteException(double saldoAtual, double valorRequerido)
        : base($"Saldo insuficiente. Atual: R${saldoAtual:F2}, Necessário: R${valorRequerido:F2}")
    {
        SaldoAtual = saldoAtual; ValorRequerido = valorRequerido;
    }
}

public class ContaInativaException : Exception
{
    public ContaInativaException(string numero) : base($"Conta {numero} inativa.") { }
}

// Registro de transação
public record Transacao(string Tipo, double Valor, double SaldoApos, DateTime Data, string Descricao)
{
    public override string ToString() =>
        $"{Data:dd/MM/yyyy HH:mm} | {Tipo,-15} | {(Valor >= 0 ? "+" : "")}{Valor,10:F2} | Saldo: {SaldoApos,10:F2} | {Descricao}";
}

// Classe abstrata base
public abstract class Conta
{
    private static int _proximoNumero = 1000;
    protected double _saldo;
    private readonly List<Transacao> _transacoes = new();

    public string Numero { get; }
    public string Titular { get; }
    public bool Ativa { get; protected set; } = true;
    public DateTime DataAbertura { get; }
    public double Saldo => _saldo;

    protected Conta(string titular, double saldoInicial = 0)
    {
        if (string.IsNullOrWhiteSpace(titular)) throw new ArgumentException("Titular inválido.");
        Numero = $"{++_proximoNumero:D6}-{new Random().Next(1, 9)}";
        Titular = titular;
        _saldo = saldoInicial;
        DataAbertura = DateTime.Now;
        if (saldoInicial > 0) RegistrarTransacao("Abertura", saldoInicial, "Depósito inicial");
    }

    protected void RegistrarTransacao(string tipo, double valor, string descricao)
    {
        _transacoes.Add(new Transacao(tipo, valor, _saldo, DateTime.Now, descricao));
    }

    public abstract string TipoConta { get; }

    // Template Method — fluxo comum de saque com hook para limite especial
    public virtual void Sacar(double valor)
    {
        ValidarAtiva();
        if (valor <= 0) throw new ArgumentException("Valor deve ser positivo.");
        if (!PodeSacar(valor)) throw new SaldoInsuficienteException(_saldo, valor);
        _saldo -= valor;
        RegistrarTransacao("Saque", -valor, "Saque realizado");
    }

    // Hook que pode ser sobrescrito por tipos que têm limite especial
    protected virtual bool PodeSacar(double valor) => valor <= _saldo;

    public void Depositar(double valor)
    {
        ValidarAtiva();
        if (valor <= 0) throw new ArgumentException("Valor deve ser positivo.");
        _saldo += valor;
        RegistrarTransacao("Depósito", valor, "Depósito realizado");
    }

    public void Transferir(Conta destino, double valor)
    {
        ValidarAtiva();
        destino.ValidarAtiva();
        Sacar(valor);
        _transacoes[^1] = _transacoes[^1] with { Descricao = $"Transferência para {destino.Titular} ({destino.Numero})" };
        destino._saldo += valor;
        destino.RegistrarTransacao("Recebimento", valor, $"Transferência de {Titular} ({Numero})");
    }

    private void ValidarAtiva()
    {
        if (!Ativa) throw new ContaInativaException(Numero);
    }

    public virtual void AplicarRendimento() { /* override nas subclasses */ }

    public void ExibirExtrato()
    {
        Console.WriteLine($"\n{'═',70}");
        Console.WriteLine($"  {TipoConta} — {Numero}");
        Console.WriteLine($"  Titular: {Titular} | Abertura: {DataAbertura:dd/MM/yyyy}");
        Console.WriteLine($"{'─',70}");
        Console.WriteLine($"  {"DATA/HORA",-20} {"TIPO",-15} {"VALOR",10} {"SALDO",12} DESCRIÇÃO");
        Console.WriteLine($"{'─',70}");
        foreach (var t in _transacoes) Console.WriteLine($"  {t}");
        Console.WriteLine($"{'─',70}");
        Console.WriteLine($"  SALDO ATUAL: R${_saldo,10:F2}");
        Console.WriteLine($"{'═',70}\n");
    }
}

// Conta Corrente — tem limite de crédito (cheque especial)
public class ContaCorrente : Conta
{
    public double LimiteCredito { get; }
    public double SaldoDisponivel => _saldo + LimiteCredito;

    public ContaCorrente(string titular, double saldoInicial = 0, double limite = 500)
        : base(titular, saldoInicial)
    {
        LimiteCredito = limite;
    }

    public override string TipoConta => "Conta Corrente";
    protected override bool PodeSacar(double valor) => valor <= SaldoDisponivel;
}

// Conta Poupança — tem rendimento mensal
public class ContaPoupanca : Conta
{
    public double TaxaRendimentoMensal { get; }
    private int _mesesRendidos = 0;

    public ContaPoupanca(string titular, double saldoInicial = 0, double taxaMensal = 0.005)
        : base(titular, saldoInicial)
    {
        TaxaRendimentoMensal = taxaMensal;
    }

    public override string TipoConta => "Conta Poupança";

    public override void AplicarRendimento()
    {
        _mesesRendidos++;
        double rendimento = _saldo * TaxaRendimentoMensal;
        _saldo += rendimento;
        RegistrarTransacao("Rendimento", rendimento,
            $"Rendimento mês {_mesesRendidos} ({TaxaRendimentoMensal:P2})");
        Console.WriteLine($"  Rendimento aplicado: +R${rendimento:F2} (taxa: {TaxaRendimentoMensal:P2})");
    }
}

// Conta Investimento — rendimento maior, mas com carência
public class ContaInvestimento : Conta
{
    public double TaxaAnual { get; }
    public int CarenciaDias { get; }
    public DateTime DataCarencia => DataAbertura.AddDays(CarenciaDias);

    public ContaInvestimento(string titular, double saldoInicial, double taxaAnual = 0.12, int carencia = 30)
        : base(titular, saldoInicial)
    {
        TaxaAnual = taxaAnual;
        CarenciaDias = carencia;
    }

    public override string TipoConta => "Conta Investimento";

    public override void Sacar(double valor)
    {
        if (DateTime.Now < DataCarencia)
            throw new InvalidOperationException($"Período de carência até {DataCarencia:dd/MM/yyyy}.");
        base.Sacar(valor);
    }

    public override void AplicarRendimento()
    {
        double taxaMensal = TaxaAnual / 12;
        double rendimento = _saldo * taxaMensal;
        _saldo += rendimento;
        RegistrarTransacao("Rendimento", rendimento, $"Rendimento mensal (taxa anual: {TaxaAnual:P0})");
        Console.WriteLine($"  Investimento: +R${rendimento:F2}");
    }
}

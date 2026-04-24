// Exercício 05 — SOLUÇÃO
class Funcionario
{
    public string Nome { get; }
    public string CPF { get; }
    public double Salario { get; private set; }
    public string Cargo { get; set; }
    public DateTime DataAdmissao { get; }

    public Funcionario(string nome, string cpf, double salario, string cargo)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome inválido.");
        var cpfNumeros = new string(cpf.Where(char.IsDigit).ToArray());
        if (cpfNumeros.Length != 11) throw new ArgumentException("CPF deve ter 11 dígitos.");
        if (salario <= 0) throw new ArgumentException("Salário deve ser positivo.");
        if (string.IsNullOrWhiteSpace(cargo)) throw new ArgumentException("Cargo inválido.");

        Nome = nome.Trim();
        CPF = cpfNumeros;
        Salario = salario;
        Cargo = cargo;
        DataAdmissao = DateTime.Now;
    }

    public double CalcularSalarioAnual() => Salario * 13.33; // com 13o e férias

    public int TempoDeEmpresa() =>
        (int)((DateTime.Now - DataAdmissao).TotalDays / 30);

    public void AplicarAumento(double percentual)
    {
        if (percentual <= 0) throw new ArgumentException("Percentual deve ser positivo.");
        Salario *= (1 + percentual / 100);
        Console.WriteLine($"Aumento de {percentual}% aplicado.");
    }

    public void ExibirFicha()
    {
        Console.WriteLine($"=== Ficha Funcional ===");
        Console.WriteLine($"Nome:    {Nome}");
        Console.WriteLine($"CPF:     {CPF[..3]}.{CPF[3..6]}.{CPF[6..9]}-{CPF[9..]}");
        Console.WriteLine($"Cargo:   {Cargo}");
        Console.WriteLine($"Salário: R${Salario:F2}");
        Console.WriteLine($"Admissão:{DataAdmissao:dd/MM/yyyy}");
    }
}

class Programa
{
    static void Main()
    {
        try
        {
            var f = new Funcionario("Ana Silva", "12345678901", 5000, "Desenvolvedora");
            f.ExibirFicha();
            f.AplicarAumento(10);
            Console.WriteLine($"Novo salário: R${f.Salario:F2}");
        }
        catch (ArgumentException ex) { Console.WriteLine($"Erro: {ex.Message}"); }

        try { var inv = new Funcionario("", "123", -100, ""); }
        catch (ArgumentException ex) { Console.WriteLine($"Erro esperado: {ex.Message}"); }
    }
}

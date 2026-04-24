// Exercício 05 — Construtor com Validação
class Funcionario
{
    // TODO: Propriedades
    // TODO: Construtor com validações (Nome, CPF, Salario)
    // TODO: CalcularSalarioAnual()
    // TODO: TempoDeEmpresa() em meses
    // TODO: ExibirFicha()
    // TODO: AplicarAumento(double percentual)
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
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }

        try { var invalido = new Funcionario("", "123", -100, ""); }
        catch (ArgumentException ex) { Console.WriteLine($"Erro esperado: {ex.Message}"); }
    }
}

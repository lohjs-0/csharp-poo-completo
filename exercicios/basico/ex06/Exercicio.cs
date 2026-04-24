// Exercício 06 — Classe Aluno com Média
class Aluno
{
    // TODO: Propriedades Nome, Matricula
    // TODO: Lista privada de notas
    // TODO: Contador estático para matrícula automática
    // TODO: AdicionarNota(double nota) — valida 0 a 10
    // TODO: CalcularMedia()
    // TODO: ObterMaiorNota() e ObterMenorNota()
    // TODO: ExibirBoletim()
}

class Programa
{
    static void Main()
    {
        var a1 = new Aluno("Ana");
        a1.AdicionarNota(8.5); a1.AdicionarNota(7.0); a1.AdicionarNota(9.5);
        a1.ExibirBoletim();

        var a2 = new Aluno("Pedro");
        a2.AdicionarNota(4.0); a2.AdicionarNota(6.0);
        a2.ExibirBoletim();
    }
}

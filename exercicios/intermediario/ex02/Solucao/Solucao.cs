// Intermediário 02 — Polimorfismo com Formas Geométricas — SOLUÇÃO

abstract class Forma
{
    public string Cor { get; set; }
    protected Forma(string cor = "Preto") { Cor = cor; }

    public abstract double CalcularArea();
    public abstract double CalcularPerimetro();
    public abstract string NomeForma { get; }

    public virtual void ExibirInfo()
    {
        Console.WriteLine($"[{NomeForma}] Área: {CalcularArea():F2} | Perímetro: {CalcularPerimetro():F2}");
    }
}

class Circulo : Forma
{
    public double Raio { get; }
    public Circulo(double raio, string cor = "Preto") : base(cor) { Raio = raio; }
    public override string NomeForma => "Círculo";
    public override double CalcularArea() => Math.PI * Raio * Raio;
    public override double CalcularPerimetro() => 2 * Math.PI * Raio;
}

class Retangulo : Forma
{
    public double Largura { get; }
    public double Altura { get; }
    public Retangulo(double l, double a, string cor = "Preto") : base(cor) { Largura = l; Altura = a; }
    public override string NomeForma => "Retângulo";
    public override double CalcularArea() => Largura * Altura;
    public override double CalcularPerimetro() => 2 * (Largura + Altura);
}

class Triangulo : Forma
{
    public double A { get; } public double B { get; } public double C { get; }
    public Triangulo(double a, double b, double c, string cor = "Preto") : base(cor) { A = a; B = b; C = c; }
    public override string NomeForma => "Triângulo";
    public override double CalcularPerimetro() => A + B + C;
    public override double CalcularArea()
    {
        double s = CalcularPerimetro() / 2;
        return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
    }
}

class Trapezio : Forma
{
    public double BaseMaior { get; } public double BaseMenor { get; }
    public double Altura { get; } public double LadoA { get; } public double LadoB { get; }

    public Trapezio(double bMaior, double bMenor, double h, double la, double lb, string cor = "Preto") : base(cor)
    { BaseMaior = bMaior; BaseMenor = bMenor; Altura = h; LadoA = la; LadoB = lb; }

    public override string NomeForma => "Trapézio";
    public override double CalcularArea() => (BaseMaior + BaseMenor) * Altura / 2;
    public override double CalcularPerimetro() => BaseMaior + BaseMenor + LadoA + LadoB;
}

static class EstatisticasFormas
{
    public static void ExibirEstatisticas(List<Forma> formas)
    {
        Console.WriteLine($"\n=== ESTATÍSTICAS ===");
        Console.WriteLine($"Total de formas: {formas.Count}");
        Console.WriteLine($"Área total: {formas.Sum(f => f.CalcularArea()):F2}");
        Console.WriteLine($"Área média: {formas.Average(f => f.CalcularArea()):F2}");
        var maiorArea = formas.MaxBy(f => f.CalcularArea());
        Console.WriteLine($"Maior área: {maiorArea?.NomeForma} ({maiorArea?.CalcularArea():F2})");
        var menorPerim = formas.MinBy(f => f.CalcularPerimetro());
        Console.WriteLine($"Menor perímetro: {menorPerim?.NomeForma} ({menorPerim?.CalcularPerimetro():F2})");
    }
}

class Programa
{
    static void Main()
    {
        var formas = new List<Forma>
        {
            new Circulo(5, "Vermelho"),
            new Retangulo(4, 6, "Azul"),
            new Triangulo(3, 4, 5, "Verde"),
            new Trapezio(8, 4, 3, 3.5, 3.5, "Amarelo"),
            new Circulo(2)
        };

        Console.WriteLine("=== FORMAS GEOMÉTRICAS ===");
        foreach (var f in formas) f.ExibirInfo();

        EstatisticasFormas.ExibirEstatisticas(formas);
    }
}

// Exercício 02 — SOLUÇÃO

class Retangulo
{
    private double _largura;
    private double _altura;

    public double Largura
    {
        get => _largura;
        set
        {
            if (value <= 0) throw new ArgumentException("Largura deve ser maior que zero.");
            _largura = value;
        }
    }

    public double Altura
    {
        get => _altura;
        set
        {
            if (value <= 0) throw new ArgumentException("Altura deve ser maior que zero.");
            _altura = value;
        }
    }

    public string Cor { get; set; }

    public Retangulo(double largura, double altura) : this(largura, altura, "Preto") { }

    public Retangulo(double largura, double altura, string cor)
    {
        Largura = largura;
        Altura = altura;
        Cor = cor;
    }

    public double CalcularArea() => Largura * Altura;
    public double CalcularPerimetro() => 2 * (Largura + Altura);
    public bool EhQuadrado() => Largura == Altura;

    public void EscalarPor(double fator)
    {
        if (fator <= 0) throw new ArgumentException("Fator deve ser positivo.");
        Largura *= fator;
        Altura *= fator;
    }

    public void ExibirInfo()
    {
        Console.WriteLine($"Retângulo: {Largura} x {Altura} | Cor: {Cor}");
        Console.WriteLine($"Área: {CalcularArea():F2}");
        Console.WriteLine($"Perímetro: {CalcularPerimetro():F2}");
    }
}

class Programa
{
    static void Main()
    {
        var ret = new Retangulo(5, 3, "Azul");
        ret.ExibirInfo();
        Console.WriteLine($"É quadrado? {ret.EhQuadrado()}");

        Console.WriteLine("\nApós escalar por 2:");
        ret.EscalarPor(2);
        ret.ExibirInfo();

        var quadrado = new Retangulo(4, 4);
        Console.WriteLine($"\nÉ quadrado? {quadrado.EhQuadrado()}");

        try { var invalido = new Retangulo(-1, 5); }
        catch (ArgumentException ex) { Console.WriteLine($"\nErro esperado: {ex.Message}"); }
    }
}

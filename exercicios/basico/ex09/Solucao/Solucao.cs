// Exercício 09 — SOLUÇÃO
class Veiculo
{
    public string Marca { get; }
    public string Modelo { get; }
    public int Ano { get; }
    public string Cor { get; set; }
    public double KmRodados { get; private set; }
    public double NivelCombustivel { get; private set; }

    private const double ConsumoPor100Km = 10.0; // 10% do tanque por 100km

    public Veiculo(string marca, string modelo, int ano, string cor)
    {
        Marca = marca; Modelo = modelo; Ano = ano; Cor = cor;
        KmRodados = 0; NivelCombustivel = 0;
    }

    public void Abastecer(double litros)
    {
        if (litros <= 0) throw new ArgumentException("Valor deve ser positivo.");
        NivelCombustivel = Math.Min(NivelCombustivel + litros, 100);
        Console.WriteLine($"Abastecido! Nível: {NivelCombustivel:F1}%");
    }

    public void Dirigir(double km)
    {
        if (km <= 0) throw new ArgumentException("Distância deve ser positiva.");
        double consumo = (km / 100) * ConsumoPor100Km;
        if (consumo > NivelCombustivel) throw new InvalidOperationException("Combustível insuficiente!");
        NivelCombustivel -= consumo;
        KmRodados += km;
        Console.WriteLine($"Dirigiu {km}km. Combustível restante: {NivelCombustivel:F1}%");
    }

    public void ExibirStatus()
    {
        Console.WriteLine($"🚗 {Ano} {Marca} {Modelo} ({Cor})");
        Console.WriteLine($"   KM: {KmRodados:F0} | Combustível: {NivelCombustivel:F1}%");
    }
}

class Programa
{
    static void Main()
    {
        var carro = new Veiculo("Toyota", "Corolla", 2022, "Prata");
        carro.ExibirStatus();
        carro.Abastecer(50);
        carro.Dirigir(200);
        carro.ExibirStatus();
    }
}

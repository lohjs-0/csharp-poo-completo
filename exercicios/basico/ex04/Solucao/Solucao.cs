// Exercício 04 — SOLUÇÃO

class Termostato
{
    private double _temperatura;
    private string _nome;

    public bool Ligado { get; private set; }

    public string Nome
    {
        get => _nome;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Nome não pode ser vazio.");
            _nome = value.Trim();
        }
    }

    public double Temperatura
    {
        get => _temperatura;
        set
        {
            if (value < -50 || value > 100)
                throw new ArgumentOutOfRangeException("Temperatura deve ser entre -50 e 100°C.");
            _temperatura = value;
        }
    }

    public double TemperaturaFahrenheit => _temperatura * 9 / 5 + 32;
    public double TemperaturaKelvin => _temperatura + 273.15;

    public Termostato(string nome, double temperaturaInicial = 20)
    {
        Nome = nome;
        Temperatura = temperaturaInicial;
        Ligado = false;
    }

    public void Ligar() { Ligado = true; Console.WriteLine($"{Nome} ligado."); }
    public void Desligar() { Ligado = false; Console.WriteLine($"{Nome} desligado."); }

    public void AumentarTemperatura(double graus)
    {
        if (!Ligado) { Console.WriteLine("Termostato desligado!"); return; }
        Temperatura = Math.Min(_temperatura + graus, 100);
        Console.WriteLine($"Temperatura: {_temperatura}°C");
    }

    public void DiminuirTemperatura(double graus)
    {
        if (!Ligado) { Console.WriteLine("Termostato desligado!"); return; }
        Temperatura = Math.Max(_temperatura - graus, -50);
        Console.WriteLine($"Temperatura: {_temperatura}°C");
    }

    public string Status() =>
        $"{Nome}: {(Ligado ? "LIGADO" : "DESLIGADO")} | {_temperatura}°C";
}

class Programa
{
    static void Main()
    {
        var t = new Termostato("Sala de Estar", 20);
        Console.WriteLine(t.Status());
        t.Ligar();
        t.AumentarTemperatura(5);
        Console.WriteLine($"Celsius: {t.Temperatura}°C");
        Console.WriteLine($"Fahrenheit: {t.TemperaturaFahrenheit:F1}°F");
        Console.WriteLine($"Kelvin: {t.TemperaturaKelvin:F1}K");
        Console.WriteLine(t.Status());
        t.Desligar();
        t.AumentarTemperatura(10);
    }
}

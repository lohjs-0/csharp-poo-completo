// Exercício 04 — Encapsulamento com Propriedades
// Implemente a classe Termostato conforme o README

class Termostato
{
    // TODO: Implemente as propriedades com validação
    // TODO: Implemente os métodos
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
        t.AumentarTemperatura(10); // deve avisar que está desligado
    }
}

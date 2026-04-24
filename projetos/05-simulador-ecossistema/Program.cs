using SimuladorEcossistema.Models;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("╔══════════════════════════════════════╗");
Console.WriteLine("║   🌿  Simulador de Ecossistema       ║");
Console.WriteLine("╚══════════════════════════════════════╝\n");

var ecossistema = new Ecossistema();

var seres = new List<SerVivo>
{
    new Carnivoro("Lobo-Alfa"),
    new Carnivoro("Lobo-Beta"),
    new Carnivoro("Lobo-Gama"),
    new Herbivoro("Coelho-01"),
    new Herbivoro("Coelho-02"),
    new Herbivoro("Coelho-03"),
    new Herbivoro("Coelho-04"),
    new Herbivoro("Coelho-05"),
    new Planta("Carvalho"),
    new Planta("Samambaia"),
    new Planta("Bambu"),
    new Planta("Rosa"),
    new Planta("Pinheiro"),
};

foreach (var ser in seres)
    ecossistema.AdicionarHabitante(ser);

static void Separador(string texto)
{
    Console.WriteLine($"\n{"─",50}");
    Console.WriteLine($"  {texto}");
    Console.WriteLine($"{"─",50}");
}

Console.WriteLine("=== HABITANTES INICIAIS ===");
Console.WriteLine("  🐺 Lobos:    3");
Console.WriteLine("  🐇 Coelhos:  5");
Console.WriteLine("  🌿 Plantas:  5");

Console.WriteLine("\n╔══════════════════════════════════════╗");
Console.WriteLine("║       INÍCIO DA SIMULAÇÃO!           ║");
Console.WriteLine("╚══════════════════════════════════════╝");

int maxRodadas = 8;
int rodada = 0;

while (rodada < maxRodadas)
{
    rodada++;
    Separador($"RODADA {rodada}");
    ecossistema.SimularRodada();

    if (rodada < maxRodadas)
    {
        Console.WriteLine("\nPressione ENTER para a próxima rodada...");
        Console.ReadLine();
    }
}

Console.WriteLine("\n\n╔══════════════════════════════════════╗");
Console.WriteLine("║        FIM DA SIMULAÇÃO!             ║");
Console.WriteLine("╚══════════════════════════════════════╝");
Console.WriteLine("  Obrigado por simular o ecossistema!");
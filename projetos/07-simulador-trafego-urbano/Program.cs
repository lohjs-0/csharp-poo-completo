using SimuladorTrafego.Models;
using SimuladorTrafego.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("╔══════════════════════════════════════╗");
Console.WriteLine("║   🚦  Simulador de Tráfego Urbano    ║");
Console.WriteLine("╚══════════════════════════════════════╝\n");

var sim = new TrafegSimulacaoService();

static void Separador(string texto)
{
    Console.WriteLine($"\n{"─",50}");
    Console.WriteLine($"  {texto}");
    Console.WriteLine($"{"─",50}");
}

Console.WriteLine("=== CIDADE: SIMULÓPOLIS ===");
Console.WriteLine("  🚗 Carros: 3   🏍️  Motos: 2");
Console.WriteLine("  🛣️  Ruas : Av. Principal | Rua B");
Console.WriteLine("  🔀 Cruzamento: Centro");

Console.WriteLine("\n╔══════════════════════════════════════╗");
Console.WriteLine("║       INÍCIO DA SIMULAÇÃO!           ║");
Console.WriteLine("╚══════════════════════════════════════╝");

int totalCiclos = 6;

for (int c = 1; c <= totalCiclos; c++)
{
    Separador($"CICLO {c}");
    sim.SimularCiclo();

    if (c < totalCiclos)
    {
        Console.WriteLine("\nPressione ENTER para o próximo ciclo...");
        Console.ReadLine();
    }
}

Console.WriteLine("\n\n╔══════════════════════════════════════╗");
Console.WriteLine("║      FIM DA SIMULAÇÃO DE TRÁFEGO!    ║");
Console.WriteLine("╚══════════════════════════════════════╝");
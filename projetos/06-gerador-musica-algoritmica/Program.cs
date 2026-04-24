using GeradorMusica.Models;
using GeradorMusica.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("╔══════════════════════════════════════╗");
Console.WriteLine("║   🎵  Gerador de Música Algorítmica  ║");
Console.WriteLine("╚══════════════════════════════════════╝\n");

var gerador = new GeradorMelodiaService();

var instrumentos = new List<Instrumento> { new Piano(), new Flauta(), new Bateria() };
var escalas      = new List<(Escala escala, string nome)>
{
    (Escala.DoMaior,    "Dó Maior"),
    (Escala.LaMemor,    "Lá Menor"),
    (Escala.Pentatonica,"Pentatônica"),
};
var algoritmos = new List<(Algoritmo alg, string nome)>
{
    (Algoritmo.Aleatorio,  "Aleatório"),
    (Algoritmo.Sequencial, "Sequencial"),
    (Algoritmo.Padrao,     "Padrão"),
};

static void Separador(string texto)
{
    Console.WriteLine($"\n{"─",50}");
    Console.WriteLine($"  {texto}");
    Console.WriteLine($"{"─",50}");
}

int composicao = 1;

foreach (var instrumento in instrumentos)
{
    foreach (var (escala, nomeEscala) in escalas)
    {
        foreach (var (algoritmo, nomeAlg) in algoritmos)
        {
            Separador($"COMPOSIÇÃO {composicao}");
            Console.WriteLine($"  🎹 Instrumento : {instrumento.Nome}");
            Console.WriteLine($"  🎼 Escala      : {nomeEscala}");
            Console.WriteLine($"  ⚙️  Algoritmo   : {nomeAlg}");

            var seq = gerador.Gerar(escala, algoritmo, quantidadeNotas: 8);
            seq.ExibirPartitura();
            instrumento.TocarSequencia(seq.Notas);

            Console.WriteLine($"\n  💾 Exportado: {seq.Exportar()}");

            composicao++;
            Console.WriteLine("\nPressione ENTER para a próxima composição...");
            Console.ReadLine();
        }
    }
}

Console.WriteLine("\n╔══════════════════════════════════════╗");
Console.WriteLine("║     FIM DAS COMPOSIÇÕES! 🎶          ║");
Console.WriteLine("╚══════════════════════════════════════╝");
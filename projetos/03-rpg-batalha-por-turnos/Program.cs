using RPG.Models;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("╔═════════════════════════════════╗");
Console.WriteLine("║   ⚔️  RPG — Batalha por Turnos  ║");
Console.WriteLine("╚═════════════════════════════════╝\n");

// Criar personagens
var herois = new List<Personagem>
{
    new Guerreiro("Thorin"),
    new Mago("Gandalf"),
    new Arqueiro("Legolas"),
    new Curandeiro("Elrond")
};

var inimigos = new List<Personagem>
{
    new Guerreiro("Orco"),
    new Mago("Bruxa"),
    new Arqueiro("Goblin")
};

static void ExibirTimeCompleto(string label, List<Personagem> time)
{
    Console.WriteLine($"\n  {label}:");
    foreach (var p in time) p.ExibirStatus();
}

static void ExibirSeparador(string texto)
{
    Console.WriteLine($"\n{'─',50}");
    Console.WriteLine($"  {texto}");
    Console.WriteLine($"{'─',50}");
}

Console.WriteLine("=== PERSONAGENS ===");
ExibirTimeCompleto("🛡️ Heróis", herois);
ExibirTimeCompleto("💀 Inimigos", inimigos);

Console.WriteLine("\n\n╔══════════════════════════════════╗");
Console.WriteLine("║      INÍCIO DA BATALHA!          ║");
Console.WriteLine("╚══════════════════════════════════╝");

var rng = new Random();
int turno = 1;

while (herois.Any(h => h.EstaVivo) && inimigos.Any(i => i.EstaVivo))
{
    ExibirSeparador($"TURNO {turno}");
    ExibirTimeCompleto("Heróis", herois);
    ExibirTimeCompleto("Inimigos", inimigos);
    Console.WriteLine();

    // Ordenar por velocidade (turnos simultâneos baseado em velocidade)
    var participantes = herois.Concat(inimigos)
        .Where(p => p.EstaVivo)
        .OrderByDescending(p => p.Velocidade)
        .ToList();

    foreach (var atacante in participantes)
    {
        if (!atacante.EstaVivo) continue;

        bool ehHeroi = herois.Contains(atacante);
        var aliados = ehHeroi ? herois : inimigos;
        var oponentes = ehHeroi ? inimigos : herois;

        var alvosVivos = oponentes.Where(p => p.EstaVivo).ToList();
        var aliadosVivos = aliados.Where(p => p.EstaVivo).ToList();

        if (!alvosVivos.Any()) break;

        // IA simples: 25% chance de usar habilidade, curandeiro cura aliado com menos HP
        if (atacante is Curandeiro && aliadosVivos.Any())
        {
            var aliadoMaisLesado = aliadosVivos.MinBy(a => (double)a.HP / a.HPMax)!;
            double percHP = (double)aliadoMaisLesado.HP / aliadoMaisLesado.HPMax;

            if (percHP < 0.5)
            {
                Console.Write($"\n  {atacante.Nome} → Cura {aliadoMaisLesado.Nome}:");
                atacante.UsarHabilidade(aliadoMaisLesado);
            }
            else
            {
                var alvo = alvosVivos[rng.Next(alvosVivos.Count)];
                Console.Write($"\n  {atacante.Nome} → {alvo.Nome}:");
                atacante.Atacar(alvo);
            }
        }
        else
        {
            var alvo = alvosVivos[rng.Next(alvosVivos.Count)];
            Console.WriteLine($"\n  {atacante.Nome} vs {alvo.Nome}:");
            if (rng.Next(100) < 25)
                atacante.UsarHabilidade(alvo);
            else
                atacante.Atacar(alvo);
        }

        atacante.TickCooldown();
        atacante.RegenerarMana(5);
    }

    turno++;
    if (turno > 20) { Console.WriteLine("\n⏰ Batalha encerrada por limite de turnos!"); break; }
    Console.WriteLine("\nPressione ENTER para o próximo turno...");
    Console.ReadLine();
}

Console.WriteLine("\n\n╔══════════════════════════════════╗");
Console.WriteLine("║        FIM DA BATALHA!           ║");
Console.WriteLine("╚══════════════════════════════════╝");

if (herois.Any(h => h.EstaVivo))
    Console.WriteLine("🏆 HERÓIS VENCERAM!");
else if (inimigos.Any(i => i.EstaVivo))
    Console.WriteLine("💀 INIMIGOS VENCERAM!");
else
    Console.WriteLine("⚔️ EMPATE!");

Console.WriteLine("\nEstado final:");
ExibirTimeCompleto("Heróis", herois);
ExibirTimeCompleto("Inimigos", inimigos);

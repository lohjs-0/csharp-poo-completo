namespace SimuladorEcossistema.Models;

public class Ecossistema
{
    private List<SerVivo> _habitantes = new();
    private Random _random = new();
    public int Rodada { get; private set; } = 0;

    public void AdicionarHabitante(SerVivo ser) => _habitantes.Add(ser);

    public void SimularRodada()
    {
        Rodada++;
        Console.WriteLine($"\n{'=',1} Rodada {Rodada} {'=',1}");

        foreach (var ser in _habitantes.Where(s => s.EstaVivo).ToList())
        {
            ser.Envelhecer();
            ser.Alimentar();

            if (ser is Carnivoro carnivoro)
            {
                var presa = _habitantes
                    .OfType<Herbivoro>()
                    .Where(h => h.EstaVivo)
                    .OrderBy(_ => _random.Next())
                    .FirstOrDefault();

                if (presa != null)
                    carnivoro.Caçar(presa);
            }

            var filho = ser.Reproduzir();
            if (filho != null)
            {
                _habitantes.Add(filho);
                Console.WriteLine($"  🐣 Novo ser nasceu: {filho.Nome}");
            }
        }

        EventoAleatorio();
        RemoverMortos();
        ExibirStatus();
    }

    private void EventoAleatorio()
    {
        int chance = _random.Next(100);
        if (chance < 15)
        {
            Console.WriteLine("  ⚡ EVENTO: Tempestade! Todos os seres perdem 10 de saúde.");
            foreach (var ser in _habitantes.Where(s => s.EstaVivo))
                ser.GetType().GetProperty("Saude")?.SetValue(ser,
                    Math.Max(0, (int)(ser.Saude) - 10));
        }
        else if (chance < 25)
        {
            Console.WriteLine("  ☀️  EVENTO: Estação farta! Plantas ganham saúde extra.");
            foreach (var planta in _habitantes.OfType<Planta>().Where(p => p.EstaVivo))
                planta.Alimentar();
        }
    }

    private void RemoverMortos()
    {
        int mortos = _habitantes.Count(s => !s.EstaVivo);
        if (mortos > 0)
            Console.WriteLine($"  💀 {mortos} ser(es) morreram esta rodada.");
        _habitantes.RemoveAll(s => !s.EstaVivo);
    }

    private void ExibirStatus()
    {
        int lobos = _habitantes.OfType<Carnivoro>().Count();
        int coelhos = _habitantes.OfType<Herbivoro>().Count();
        int plantas = _habitantes.OfType<Planta>().Count();
        Console.WriteLine($"\n  🐺 Lobos: {lobos}  |  🐇 Coelhos: {coelhos}  |  🌿 Plantas: {plantas}");
    }
}
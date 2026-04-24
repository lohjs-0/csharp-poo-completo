using SimuladorTrafego.Models;

namespace SimuladorTrafego.Services;

public class TrafegSimulacaoService
{
    private readonly Cidade _cidade;
    private readonly List<Veiculo> _veiculos = new();
    private readonly Random _random = new();

    public TrafegSimulacaoService()
    {
        _cidade = new Cidade("Simulópolis");
        Configurar();
    }

    private void Configurar()
    {
        var av1 = new Rua("Av. Principal", 4);
        var ruaB = new Rua("Rua B", 3);
        _cidade.AdicionarRua(av1);
        _cidade.AdicionarRua(ruaB);

        var cruz = new Cruzamento("Centro");
        cruz.AdicionarRua(av1);
        cruz.AdicionarRua(ruaB);
        _cidade.AdicionarCruzamento(cruz);

        for (int i = 1; i <= 3; i++) _veiculos.Add(new Carro($"Carro-{i:D2}"));
        for (int i = 1; i <= 2; i++) _veiculos.Add(new Moto($"Moto-{i:D2}"));

        foreach (var v in _veiculos)
        {
            var rua = _cidade.Ruas[_random.Next(_cidade.Ruas.Count)];
            rua.AdicionarVeiculo(v);
        }
    }

    public void SimularCiclo()
    {
        foreach (var cruzamento in _cidade.Cruzamentos)
            cruzamento.Simular();

        foreach (var rua in _cidade.Ruas)
        {
            foreach (var veiculo in rua.Veiculos)
            {
                var semaforo = _cidade.Cruzamentos
                    .FirstOrDefault(cr => cr.Ruas.Contains(rua))?.Semaforo;

                if (semaforo?.PodePassar == true)
                {
                    veiculo.Acelerar();
                    Console.WriteLine($"  {veiculo} avança na {rua.Nome}");
                }
                else
                {
                    veiculo.Frear();
                    Console.WriteLine($"  {veiculo} para na {rua.Nome}");
                }
            }
        }

        EventoAleatorio();
        ExibirCongestionamento();
    }

    private void EventoAleatorio()
    {
        if (_random.Next(100) < 20)
        {
            var rua = _cidade.Ruas[_random.Next(_cidade.Ruas.Count)];
            Console.WriteLine($"  ⚠️  EVENTO: Acidente na {rua.Nome}! Veículos freando.");
            foreach (var v in rua.Veiculos) v.Frear();
        }
    }

    private void ExibirCongestionamento()
    {
        Console.WriteLine("\n  📊 Congestionamento:");
        foreach (var rua in _cidade.Ruas)
            Console.WriteLine($"    {rua.Nome}: {rua.BarraCongestionamento()}");
    }
}
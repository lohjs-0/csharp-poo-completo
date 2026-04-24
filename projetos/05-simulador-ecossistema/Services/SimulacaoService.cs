using SimuladorEcossistema.Models;

namespace SimuladorEcossistema.Services;

public class SimulacaoService
{
    private readonly Ecossistema _ecossistema;

    public SimulacaoService()
    {
        _ecossistema = new Ecossistema();
        Popular();
    }

    private void Popular()
    {
        for (int i = 1; i <= 3; i++)
            _ecossistema.AdicionarHabitante(new Carnivoro($"Lobo-{i:D2}"));
        for (int i = 1; i <= 10; i++)
            _ecossistema.AdicionarHabitante(new Herbivoro($"Coelho-{i:D2}"));
        for (int i = 1; i <= 20; i++)
            _ecossistema.AdicionarHabitante(new Planta($"Planta-{i:D2}"));
    }

    public void Iniciar(int rodadas = 5)
    {
        Console.WriteLine("🌿 === Simulador de Ecossistema Virtual ===\n");
        for (int i = 0; i < rodadas; i++)
            _ecossistema.SimularRodada();
        Console.WriteLine("\n🏁 Simulação encerrada!");
    }
}
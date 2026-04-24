using GeradorMusica.Models;

namespace GeradorMusica.Services;

public enum Escala { DoMaior, LaMemor, Pentatonica }
public enum Algoritmo { Aleatorio, Sequencial, Padrao }

public class GeradorMelodiaService
{
    private static readonly Dictionary<Escala, List<(string nome, double freq)>> _escalas = new()
    {
        [Escala.DoMaior] = new()
        {
            ("Dó4", 261.63), ("Ré4", 293.66), ("Mi4", 329.63),
            ("Fá4", 349.23), ("Sol4", 392.00), ("Lá4", 440.00), ("Si4", 493.88)
        },
        [Escala.LaMemor] = new()
        {
            ("Lá3", 220.00), ("Si3", 246.94), ("Dó4", 261.63),
            ("Ré4", 293.66), ("Mi4", 329.63), ("Fá4", 349.23), ("Sol4", 392.00)
        },
        [Escala.Pentatonica] = new()
        {
            ("Dó4", 261.63), ("Ré4", 293.66), ("Mi4", 329.63),
            ("Sol4", 392.00), ("Lá4", 440.00)
        }
    };

    private readonly Random _random = new();

    public Sequenciador Gerar(Escala escala, Algoritmo algoritmo, int quantidadeNotas = 8)
    {
        var sequenciador = new Sequenciador();
        var notas = _escalas[escala];

        Console.WriteLine($"\n⚙️  Gerando com escala [{escala}] e algoritmo [{algoritmo}]...");

        for (int i = 0; i < quantidadeNotas; i++)
        {
            var (nome, freq) = algoritmo switch
            {
                Algoritmo.Aleatorio   => notas[_random.Next(notas.Count)],
                Algoritmo.Sequencial  => notas[i % notas.Count],
                Algoritmo.Padrao      => notas[i % 2 == 0 ? 0 : _random.Next(notas.Count)],
                _                     => notas[0]
            };

            double duracao = new[] { 0.25, 0.5, 1.0 }[_random.Next(3)];
            sequenciador.AdicionarNota(new NotaMusical(nome, freq, duracao));
        }

        return sequenciador;
    }
}
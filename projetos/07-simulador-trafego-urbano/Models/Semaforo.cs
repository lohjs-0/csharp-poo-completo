namespace SimuladorTrafego.Models;

public enum CorSemaforo { Verde, Amarelo, Vermelho }

public class Semaforo
{
    public CorSemaforo Cor { get; private set; } = CorSemaforo.Verde;
    private int _ciclo = 0;

    public void Avançar()
    {
        _ciclo++;
        Cor = (_ciclo % 6) switch
        {
            0 or 1 or 2 => CorSemaforo.Verde,
            3            => CorSemaforo.Amarelo,
            _            => CorSemaforo.Vermelho,
        };
    }

    public string Icone => Cor switch
    {
        CorSemaforo.Verde     => "🟢",
        CorSemaforo.Amarelo   => "🟡",
        CorSemaforo.Vermelho  => "🔴",
        _                     => "⚫"
    };

    public bool PodePassar => Cor == CorSemaforo.Verde;
}

public class Rua
{
    public string Nome { get; }
    public int Capacidade { get; }
    public List<Veiculo> Veiculos { get; } = new();

    public Rua(string nome, int capacidade = 5)
    {
        Nome = nome;
        Capacidade = capacidade;
    }

    public bool AdicionarVeiculo(Veiculo v)
    {
        if (Veiculos.Count >= Capacidade) return false;
        v.RuaAtual = Nome;
        Veiculos.Add(v);
        return true;
    }

    public double Congestionamento => (double)Veiculos.Count / Capacidade;

    public string BarraCongestionamento()
    {
        int cheio = (int)(Congestionamento * 10);
        return "[" + new string('█', cheio) + new string('░', 10 - cheio) + $"] {Congestionamento:P0}";
    }
}

public class Cruzamento
{
    public string Nome { get; }
    public Semaforo Semaforo { get; } = new();
    public List<Rua> Ruas { get; } = new();

    public Cruzamento(string nome) => Nome = nome;

    public void AdicionarRua(Rua rua) => Ruas.Add(rua);

    public void Simular()
    {
        Semaforo.Avançar();
        Console.WriteLine($"  {Semaforo.Icone} Semáforo [{Nome}] → {Semaforo.Cor.ToString().ToUpper()}");
    }
}

public class Cidade
{
    public string Nome { get; }
    public List<Rua> Ruas { get; } = new();
    public List<Cruzamento> Cruzamentos { get; } = new();

    public Cidade(string nome) => Nome = nome;

    public void AdicionarRua(Rua rua) => Ruas.Add(rua);
    public void AdicionarCruzamento(Cruzamento c) => Cruzamentos.Add(c);
}
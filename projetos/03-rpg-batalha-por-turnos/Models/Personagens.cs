namespace RPG.Models;

public enum StatusPersonagem { Vivo, Derrotado, Paralizado }

public interface IHabilidadeEspecial
{
    string Nome { get; }
    string Descricao { get; }
    int CustoMana { get; }
    int Cooldown { get; }
    void Usar(Personagem usuario, Personagem alvo);
}

public abstract class Personagem
{
    protected static readonly Random Rng = new();

    public string Nome { get; }
    public string Classe { get; }
    public int Nivel { get; protected set; } = 1;
    public int HP { get; protected set; }
    public int HPMax { get; protected set; }
    public int Mana { get; protected set; }
    public int ManaMax { get; protected set; }
    public int Forca { get; protected set; }
    public int Defesa { get; protected set; }
    public int Velocidade { get; protected set; }
    public StatusPersonagem Status { get; protected set; } = StatusPersonagem.Vivo;
    public bool EstaVivo => Status == StatusPersonagem.Vivo;

    protected IHabilidadeEspecial? Habilidade;
    private int _cooldownAtual = 0;

    protected Personagem(string nome, string classe, int hp, int mana, int forca, int defesa, int velocidade)
    {
        Nome = nome; Classe = classe;
        HP = HPMax = hp; Mana = ManaMax = mana;
        Forca = forca; Defesa = defesa; Velocidade = velocidade;
    }

    public virtual int Atacar(Personagem alvo)
    {
        int dano = Math.Max(1, Forca - alvo.Defesa / 2 + Rng.Next(-3, 4));
        bool critico = Rng.Next(100) < 15;
        if (critico) { dano = (int)(dano * 1.5); }

        alvo.ReceberDano(dano);
        string criticoTxt = critico ? " (CRÍTICO!)" : "";
        Console.WriteLine($"    ⚔️  {Nome} ataca {alvo.Nome} por {dano} de dano{criticoTxt}");
        return dano;
    }

    public virtual void ReceberDano(int dano)
    {
        int danoReal = Math.Max(1, dano);
        HP = Math.Max(0, HP - danoReal);
        if (HP == 0) { Status = StatusPersonagem.Derrotado; Console.WriteLine($"    💀 {Nome} foi derrotado!"); }
    }

    public virtual void Curar(int quantidade)
    {
        int cura = Math.Min(quantidade, HPMax - HP);
        HP += cura;
        Console.WriteLine($"    💚 {Nome} recuperou {cura} HP");
    }

    public void UsarHabilidade(Personagem alvo)
    {
        if (Habilidade == null) { Console.WriteLine($"    {Nome} não tem habilidade especial."); return; }
        if (_cooldownAtual > 0) { Console.WriteLine($"    ⏳ Habilidade em cooldown ({_cooldownAtual} turnos)"); return; }
        if (Mana < Habilidade.CustoMana) { Console.WriteLine($"    ❌ Mana insuficiente ({Mana}/{Habilidade.CustoMana})"); return; }
        Mana -= Habilidade.CustoMana;
        _cooldownAtual = Habilidade.Cooldown;
        Console.WriteLine($"    ✨ {Nome} usa {Habilidade.Nome}!");
        Habilidade.Usar(this, alvo);
    }

    public void TickCooldown() { if (_cooldownAtual > 0) _cooldownAtual--; }

    public void RegenerarMana(int quantidade) => Mana = Math.Min(ManaMax, Mana + quantidade);

    public string BarraVida()
    {
        int barras = (int)((double)HP / HPMax * 10);
        return $"[{"█".PadRight(barras),-10}] {HP}/{HPMax}";
    }

    public virtual void ExibirStatus()
    {
        string statusIcon = Status switch
        {
            StatusPersonagem.Vivo => "💚",
            StatusPersonagem.Derrotado => "💀",
            _ => "⚡"
        };
        Console.WriteLine($"  {statusIcon} {Nome,-12} [{Classe,-10}] HP:{BarraVida()} MP:{Mana}/{ManaMax} Lv:{Nivel}");
    }
}

// ── Classes concretas ──

public class Guerreiro : Personagem
{
    public Guerreiro(string nome) : base(nome, "Guerreiro", hp: 120, mana: 30, forca: 18, defesa: 12, velocidade: 6)
    {
        Habilidade = new GolpeBrutal();
    }
}

public class Mago : Personagem
{
    public Mago(string nome) : base(nome, "Mago", hp: 60, mana: 100, forca: 8, defesa: 5, velocidade: 7)
    {
        Habilidade = new BolaDeFogo();
    }
    public override int Atacar(Personagem alvo)
    {
        int dano = Forca * 2 + Rng.Next(-2, 5); // magia ignora metade da defesa
        Console.Write("    🔮 ");
        alvo.ReceberDano(dano);
        Console.WriteLine($"    🔮 {Nome} lança magia em {alvo.Nome} por {dano} de dano");
        return dano;
    }
}

public class Arqueiro : Personagem
{
    public Arqueiro(string nome) : base(nome, "Arqueiro", hp: 80, mana: 50, forca: 14, defesa: 7, velocidade: 12)
    {
        Habilidade = new ChuvaDeFlechas();
    }
    public override int Atacar(Personagem alvo)
    {
        // Arqueiro tem chance de atacar duas vezes
        int dano = base.Atacar(alvo);
        if (Rng.Next(100) < 40 && alvo.EstaVivo)
        {
            Console.WriteLine($"    🏹 {Nome} ataca novamente!");
            dano += base.Atacar(alvo);
        }
        return dano;
    }
}

public class Curandeiro : Personagem
{
    public Curandeiro(string nome) : base(nome, "Curandeiro", hp: 70, mana: 120, forca: 7, defesa: 6, velocidade: 8)
    {
        Habilidade = new CuraDivina();
    }
}

// ── Habilidades ──

public class GolpeBrutal : IHabilidadeEspecial
{
    public string Nome => "Golpe Brutal";
    public string Descricao => "Ataque poderoso que causa dano triplo";
    public int CustoMana => 15;
    public int Cooldown => 3;
    public void Usar(Personagem usuario, Personagem alvo)
    {
        int dano = usuario.Forca * 3;
        Console.WriteLine($"    💥 Golpe Brutal causa {dano} de dano!");
        alvo.ReceberDano(dano);
    }
}

public class BolaDeFogo : IHabilidadeEspecial
{
    public string Nome => "Bola de Fogo";
    public string Descricao => "Projétil de fogo que causa dano massivo";
    public int CustoMana => 30;
    public int Cooldown => 2;
    public void Usar(Personagem usuario, Personagem alvo)
    {
        int dano = usuario.Forca * 4 + 10;
        Console.WriteLine($"    🔥 Bola de Fogo explode por {dano} de dano!");
        alvo.ReceberDano(dano);
    }
}

public class ChuvaDeFlechas : IHabilidadeEspecial
{
    public string Nome => "Chuva de Flechas";
    public string Descricao => "Dispara 3 flechas consecutivas";
    public int CustoMana => 20;
    public int Cooldown => 2;
    public void Usar(Personagem usuario, Personagem alvo)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (!alvo.EstaVivo) break;
            int dano = usuario.Forca / 2 + new Random().Next(3, 8);
            Console.WriteLine($"    🏹 Flecha {i}: {dano} de dano!");
            alvo.ReceberDano(dano);
        }
    }
}

public class CuraDivina : IHabilidadeEspecial
{
    public string Nome => "Cura Divina";
    public string Descricao => "Restaura uma grande quantidade de HP";
    public int CustoMana => 35;
    public int Cooldown => 3;
    public void Usar(Personagem usuario, Personagem alvo)
    {
        int cura = 50;
        Console.WriteLine($"    ✨ Cura Divina restaura {cura} HP de {alvo.Nome}!");
        alvo.Curar(cura);
    }
}

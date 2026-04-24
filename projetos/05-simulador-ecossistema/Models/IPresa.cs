namespace SimuladorEcossistema.Models;

public interface IPresa
{
    void SerAtacado(int dano);
    bool EstaVivo { get; }
    string Nome { get; }
}
namespace SimuladorEcossistema.Models;

public interface IPredador
{
    void Caçar(IPresa presa);
    int ForcaAtaque { get; }
}
// Exercício 09 — Classe Veículo
class Veiculo
{
    // TODO: Marca, Modelo, Ano, Cor
    // TODO: KmRodados (somente leitura externa)
    // TODO: NivelCombustivel (0 a 100, somente leitura externa)
    // TODO: Abastecer(double litros) — soma ao nível (máx 100)
    // TODO: Dirigir(double km) — consome combustível, lança exceção se sem combustível
    // TODO: ExibirStatus()
}

class Programa
{
    static void Main()
    {
        var carro = new Veiculo("Toyota", "Corolla", 2022, "Prata");
        carro.ExibirStatus();
        carro.Abastecer(50);
        carro.Dirigir(200);
        carro.ExibirStatus();
    }
}

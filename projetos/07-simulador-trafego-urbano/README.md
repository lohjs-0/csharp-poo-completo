# 🚦 Simulador de Tráfego Urbano

Simulação de tráfego em uma pequena cidade com carros, motos, semáforos e cruzamentos.

## 📚 Conceitos de POO Aplicados

| Conceito | Aplicação |
|---|---|
| **Herança** | `Carro` e `Moto` herdam de `Veiculo` |
| **Polimorfismo** | Diferentes velocidades e comportamentos de aceleração por veículo |
| **Encapsulamento** | Estado de cada `Veiculo` (posição, velocidade, destino) e `Semaforo` (cor, tempo) |
| **Composição** | `Cidade` é composta por `Rua`s e `Cruzamento`s |

## 🗂️ Estrutura do Projeto

```
07-simulador-trafego-urbano/
├── Models/
│   ├── Veiculo.cs       # Classe base abstrata
│   ├── Carro.cs         # Herda de Veiculo
│   ├── Moto.cs          # Herda de Veiculo
│   ├── Semaforo.cs      # Controla o fluxo de veículos
│   ├── Rua.cs           # Trecho de pista
│   ├── Cruzamento.cs    # Interseção com semáforo
│   └── Cidade.cs        # Composta por ruas e cruzamentos
├── Services/
│   └── TrafegSimulacaoService.cs
├── Program.cs
└── SimuladorTrafego.csproj
```

## ▶️ Como Executar

```bash
dotnet run
```

## 🎮 Funcionalidades

- Veículos se movem por ruas com velocidades diferentes
- Semáforos mudam de cor automaticamente a cada ciclo
- Eventos: acidentes, obras, veículos de emergência
- Relatório de congestionamento por rua

## 💡 Exemplo de Uso

```
🚦 === Simulador de Tráfego Urbano ===

=== Ciclo 1 ===
🟢 Semáforo [Av. Principal] → VERDE
🚗 Carro-01 avança (vel: 60km/h) na Av. Principal
🏍️  Moto-01 avança (vel: 90km/h) na Rua B
🔴 Semáforo [Rua B] → VERMELHO
⚠️  EVENTO: Acidente na Av. Principal! Redução de velocidade.

Congestionamento: Av. Principal [████░░] 67%
```
# Projeto 05

# 🌿 Simulador de Ecossistema Virtual

Simulação de um ecossistema onde diferentes espécies interagem, reproduzem e sobrevivem em uma cadeia alimentar.

## 📚 Conceitos de POO Aplicados

| Conceito | Aplicação |
|---|---|
| **Herança** | `Animal` e `Planta` herdam de `SerVivo`; `Carnivoro` e `Herbivoro` herdam de `Animal` |
| **Polimorfismo** | Cada espécie tem seu próprio comportamento de `Alimentar()` e `Reproduzir()` |
| **Abstração** | Classe abstrata `SerVivo` define comportamentos comuns |
| **Encapsulamento** | Estado de cada ser vivo (saúde, fome, idade) é gerenciado internamente |
| **Interfaces** | `IPredador` e `IPresa` definem interações entre espécies |

## 🗂️ Estrutura do Projeto

```
05-simulador-ecossistema/
├── Models/
│   ├── SerVivo.cs          # Classe base abstrata
│   ├── Animal.cs           # Herda de SerVivo
│   ├── Planta.cs           # Herda de SerVivo
│   ├── Carnivoro.cs        # Herda de Animal
│   ├── Herbivoro.cs        # Herda de Animal
│   ├── Ecossistema.cs      # Gerencia todos os seres vivos
│   ├── IPredador.cs        # Interface para predadores
│   └── IPresa.cs           # Interface para presas
├── Services/
│   └── SimulacaoService.cs # Lógica de simulação e eventos
├── Program.cs
└── SimuladorEcossistema.csproj
```

## ▶️ Como Executar

```bash
dotnet run
```

## 🎮 Funcionalidades

- Simulação de ciclo de vida (nascimento, envelhecimento, morte)
- Cadeia alimentar entre predadores e presas
- Reprodução entre membros da mesma espécie
- Eventos aleatórios: desastres naturais, mutações, novas espécies
- Relatório do estado do ecossistema a cada rodada

## 💡 Exemplo de Uso

```
=== Ecossistema Virtual - Rodada 1 ===
🐺 Lobos: 3  |  🐇 Coelhos: 12  |  🌿 Plantas: 30

> Lobo [Alfa] atacou Coelho [Veloz] — Coelho morreu!
> Coelho [Marrom] se reproduziu — novo coelho nasceu!
> Evento: Seca! Plantas -20%

=== Rodada 2 ===
🐺 Lobos: 3  |  🐇 Coelhos: 12  |  🌿 Plantas: 24
```

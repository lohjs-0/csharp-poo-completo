# 🎵 Gerador de Música Algorítmica

Programa que gera sequências musicais baseadas em regras algorítmicas. O usuário define escala, ritmo e instrumento, e o gerador cria uma composição única.

## 📚 Conceitos de POO Aplicados

| Conceito | Aplicação |
|---|---|
| **Herança** | `Piano`, `Bateria`, `Flauta` herdam de `Instrumento` |
| **Polimorfismo** | Cada instrumento tem seu próprio `TocarNota()` |
| **Composição** | `Sequenciador` contém várias `NotaMusical` e `Acorde` |
| **Encapsulamento** | `Frequencia`, `Duracao` e `Volume` encapsulados em `NotaMusical` |

## 🗂️ Estrutura do Projeto

```
06-gerador-musica-algoritmica/
├── Models/
│   ├── NotaMusical.cs       # Representa uma nota (frequência, duração, volume)
│   ├── Acorde.cs            # Conjunto de notas tocadas simultaneamente
│   ├── Instrumento.cs       # Classe base abstrata
│   ├── Piano.cs             # Herda de Instrumento
│   ├── Bateria.cs           # Herda de Instrumento
│   ├── Flauta.cs            # Herda de Instrumento
│   └── Sequenciador.cs      # Gerencia a sequência de notas
├── Services/
│   └── GeradorMelodiaService.cs  # Algoritmos de geração musical
├── Program.cs
└── GeradorMusica.csproj
```

## ▶️ Como Executar

```bash
dotnet run
```

## 🎮 Funcionalidades

- Geração aleatória de melodias com diferentes escalas (Maior, Menor, Pentatônica)
- Suporte a múltiplos instrumentos com timbres diferentes
- Exportação da composição em representação textual
- Algoritmos: aleatório, sequencial e baseado em padrões

## 💡 Exemplo de Uso

```
🎵 === Gerador de Música Algorítmica ===

Instrumento: Piano
Escala: Dó Maior
Algoritmo: Aleatório

Composição gerada:
| Dó4 (0.5s) | Mi4 (0.25s) | Sol4 (0.5s) | Lá4 (0.25s) |
| Mi4 (0.5s) | Ré4 (0.5s)  | Dó4 (1.0s)  |             |

♪ Piano: Dó4... Mi4... Sol4... Lá4...
```
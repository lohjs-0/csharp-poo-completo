# Exercício 02 — Classe Retângulo com Área e Perímetro

## 🎯 Objetivo
Criar uma classe com cálculos matemáticos e validações básicas.

## 📋 Requisitos

### Propriedades
- `Largura` (double) — largura do retângulo
- `Altura` (double) — altura do retângulo
- `Cor` (string) — cor do retângulo (padrão: "Preto")

### Construtores
- `Retangulo(double largura, double altura)`
- `Retangulo(double largura, double altura, string cor)`

### Métodos
- `CalcularArea()` → double
- `CalcularPerimetro()` → double
- `EhQuadrado()` → bool (true se largura == altura)
- `EscalarPor(double fator)` — multiplica largura e altura pelo fator
- `ExibirInfo()` — exibe todas as informações

### Validações
- Largura e Altura devem ser maiores que zero (lance `ArgumentException` se não forem)

## 📝 Saída Esperada
```
Retângulo: 5 x 3 | Cor: Azul
Área: 15.00
Perímetro: 16.00
É quadrado? False

Após escalar por 2:
Retângulo: 10 x 6 | Cor: Azul
Área: 60.00
```

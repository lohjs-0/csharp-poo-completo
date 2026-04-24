# Exercício 01 — Criar Classe Pessoa

## 🎯 Objetivo
Praticar a criação de uma classe simples com propriedades, construtor e métodos.

## 📋 Descrição
Crie uma classe `Pessoa` que represente uma pessoa com as seguintes características:

## Requisitos

### Propriedades
- `Nome` (string) — nome completo da pessoa
- `Idade` (int) — idade em anos
- `Email` (string) — endereço de e-mail
- `Altura` (double) — altura em metros

### Construtores
- Um construtor que receba `Nome` e `Idade`
- Um construtor que receba `Nome`, `Idade` e `Email`

### Métodos
- `Apresentar()` — exibe uma saudação com nome e idade
- `EhMaiorDeIdade()` — retorna `true` se idade >= 18
- `IMC(double peso)` — calcula e retorna o IMC (peso / altura²)
- `ClassificarIMC(double peso)` — retorna a classificação do IMC como string

### Classificação do IMC
| IMC | Classificação |
|-----|--------------|
| < 18.5 | Abaixo do peso |
| 18.5 a 24.9 | Peso normal |
| 25 a 29.9 | Sobrepeso |
| >= 30 | Obesidade |

## 📝 Exemplo de Saída Esperada
```
Olá! Meu nome é Ana Silva e tenho 25 anos.
É maior de idade? True
IMC: 22.86
Classificação: Peso normal
```

## 💡 Dicas
- Use `Math.Round()` para arredondar o IMC a 2 casas decimais
- Valide se a altura é maior que zero antes de calcular o IMC

## 📁 Arquivos
- `Exercicio.cs` — esqueleto do código para preencher
- `Solucao/Solucao.cs` — solução completa (olhe só depois de tentar!)

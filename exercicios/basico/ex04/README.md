# Exercício 04 — Encapsulamento com Propriedades

## 🎯 Objetivo
Praticar encapsulamento com getters/setters e validações.

## 📋 Requisitos
Crie a classe `Termostato`:

### Propriedades com validação
- `Temperatura` (double) — entre -50 e 100 graus Celsius
- `Nome` (string) — não pode ser vazio
- `Ligado` (bool) — padrão false

### Propriedades calculadas
- `TemperaturaFahrenheit` → converte Celsius para Fahrenheit
- `TemperaturaKelvin` → converte para Kelvin

### Métodos
- `Ligar()` / `Desligar()` 
- `AumentarTemperatura(double graus)` — só funciona se ligado
- `DiminuirTemperatura(double graus)` — só funciona se ligado
- `Status()` → string descrevendo o estado atual

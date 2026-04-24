# Exercício 03 — Conta Bancária Simples

## 🎯 Objetivo
Praticar encapsulamento e lógica de negócio básica.

## 📋 Requisitos

### Propriedades
- `Titular` (string) — somente leitura
- `Numero` (string) — somente leitura
- `Saldo` (double) — somente leitura (privado para escrita)

### Métodos
- `Depositar(double valor)` → void — valida que valor > 0
- `Sacar(double valor)` → bool — retorna false se saldo insuficiente
- `ExibirExtrato()` → void — exibe histórico de transações
- `TransferirPara(ContaBancaria destino, double valor)` → bool

### Regras de Negócio
- Saldo não pode ficar negativo
- Valor de depósito/saque deve ser maior que zero
- Guarde o histórico de todas as transações com data/hora

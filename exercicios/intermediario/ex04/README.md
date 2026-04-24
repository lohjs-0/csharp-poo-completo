# Exercício Intermediário 04

## Interface: IPagavel
Crie a interface `IPagavel` com método `ProcessarPagamento(double valor)`. Implemente: `CartaoCredito`, `CartaoDebito`, `PIX`, `Boleto`, `CriptomMoeda`. Crie um `GatewayPagamento` que aceite qualquer `IPagavel` e processe com retry (3 tentativas) em caso de falha.

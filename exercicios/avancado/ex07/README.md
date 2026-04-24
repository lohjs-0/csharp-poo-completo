# Exercício Avançado 07 — Chain of Responsibility

## 🎯 Objetivo
Implementar uma cadeia de validadores usando Chain of Responsibility.

## 📋 Requisitos
- Classe abstrata `Validador<T>` com `ProximoValidador` e método `Validar(T input)`
- Cadeia de validadores para um formulário de cadastro: `ValidadorNome`, `ValidadorEmail`, `ValidadorSenha`, `ValidadorIdade`, `ValidadorCPF`
- Cada validador decide se passa para o próximo ou retorna o erro

## 💡 Conceitos
Chain of Responsibility Pattern, Classes Abstratas Genéricas

# Exercício Avançado 03 — Observer Pattern

## 🎯 Objetivo
Implementar o padrão Observer para comunicação desacoplada entre objetos.

## 📋 Requisitos
- Interface `IObservavel<T>` com `Assinar(IObservador<T>)` e `Notificar(T evento)`
- Interface `IObservador<T>` com `Atualizar(T evento)`
- Cenário: Sistema de estoque — quando produto abaixo do mínimo, notifica Email, SMS e Dashboard
- Permitir desinscrição de observadores

## 💡 Conceitos
Observer Pattern, Interfaces genéricas, Delegates e Events

# Exercício Avançado 04 — Factory Method

## 🎯 Objetivo
Implementar o padrão Factory Method para criação de objetos.

## 📋 Requisitos
- Classe abstrata `FabricaConexao` com método abstrato `CriarConexao()`
- Implementações: `FabricaMySQL`, `FabricaSQLite`, `FabricaPostgres`
- Interface `IConexao` com `Abrir()`, `Fechar()`, `Executar(string query)`
- `FabricaConexao` estática que recebe string de configuração e retorna a fábrica correta

## 💡 Conceitos
Factory Method Pattern, Classes Abstratas, Interfaces

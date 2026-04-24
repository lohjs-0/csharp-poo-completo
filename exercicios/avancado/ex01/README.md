# Exercício Avançado 01 — Sistema de Plugins com Interfaces

## 🎯 Objetivo
Criar um sistema extensível baseado em interfaces que simula um sistema de plugins.

## 📋 Requisitos
- Interface `IPlugin` com: `Nome`, `Versao`, `Inicializar()`, `Executar(string input)`, `Finalizar()`
- Classe `GerenciadorPlugins` que carrega, inicializa e executa plugins
- Implementar pelo menos 3 plugins concretos: `PluginLogger`, `PluginCriptografia`, `PluginValidacao`
- O gerenciador deve executar os plugins em cadeia (output de um é input do próximo)

## 💡 Conceitos
Interfaces, Composição, Polimorfismo, Injeção de Dependência

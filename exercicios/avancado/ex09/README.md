# Exercício Avançado 09 — Pipeline de Processamento Genérico

## 🎯 Objetivo
Criar um pipeline fluente e genérico para processamento de dados.

## 📋 Requisitos
- Classe `Pipeline<TInput, TOutput>` com método `Adicionar<TNext>(Func<TOutput, TNext>)`
- Suporte à sintaxe fluente: `pipeline.Adicionar(passo1).Adicionar(passo2).Executar(input)`
- Pipeline de exemplo: String → Validar → Parsear → Transformar → Salvar
- Suporte a tratamento de erros em cada passo

## 💡 Conceitos
Generics avançados, Delegates, Fluent Interface, Functional Programming

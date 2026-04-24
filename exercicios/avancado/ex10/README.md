# Exercício Avançado 10 — Mini ORM Genérico

## 🎯 Objetivo
Criar um mini ORM (Object-Relational Mapper) usando reflection e generics.

## 📋 Requisitos
- Atributos personalizados: `[Tabela("nome")]`, `[Coluna("nome")]`, `[ChavePrimaria]`
- Classe `MiniORM` com métodos genéricos: `Salvar<T>(T entidade)`, `Buscar<T>(int id)`, `ListarTodos<T>()`
- O ORM deve gerar SQL automaticamente baseado nos atributos
- Simule a execução (não precisa de banco real)

## 💡 Conceitos
Reflection, Attributes, Generics avançados, Metaprogramação

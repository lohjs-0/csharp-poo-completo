# Exercício Avançado 02 — Repositório Genérico

## 🎯 Objetivo
Implementar o padrão Repository com generics.

## 📋 Requisitos
- Interface `IRepositorio<T, TId>` com CRUD completo
- `RepositorioEmMemoria<T, TId>` como implementação base
- Suporte a paginação: `Paginar(int pagina, int tamanhoPagina)`
- Suporte a filtros: `Buscar(Expression<Func<T, bool>> filtro)`
- Classes de entidade: `Produto`, `Cliente`, `Pedido`

## 💡 Conceitos
Generics, Constraints, Interfaces, LINQ

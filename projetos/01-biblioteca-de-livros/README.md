# 📚 Projeto 01 — Biblioteca de Livros

## Descrição
Sistema de gerenciamento de biblioteca com controle de empréstimos, devoluções e acervo.

## Conceitos aplicados
- Classes e Objetos
- Encapsulamento
- Listas e Coleções
- Exceções customizadas
- Datas e formatação

## Funcionalidades
- ✅ Cadastro de livros e membros
- ✅ Empréstimo com prazo de devolução
- ✅ Devolução com cálculo de multa por atraso
- ✅ Histórico de empréstimos por membro
- ✅ Busca por título, autor e gênero
- ✅ Relatório de livros mais emprestados

## Como rodar
```bash
cd projetos/01-biblioteca-de-livros
dotnet run
```

## Estrutura
```
01-biblioteca-de-livros/
├── Program.cs          → Programa principal com menu
├── Models/
│   ├── Livro.cs
│   ├── Membro.cs
│   └── Emprestimo.cs
├── Services/
│   └── BibliotecaService.cs
└── Biblioteca.csproj
```

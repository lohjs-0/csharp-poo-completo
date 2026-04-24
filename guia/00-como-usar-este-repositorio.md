# 00 — Como Usar Este Repositório

Bem-vindo! Antes de começar, leia este guia para aproveitar ao máximo o repositório.

---

## 🎯 Para quem é este repositório?

- Quem está começando em C# e quer aprender POO do zero
- Quem já programa em outra linguagem e quer aprender POO com C#
- Quem já sabe o básico e quer consolidar conhecimentos com exercícios e projetos
- Para quem se interessar

---

## 📋 Como o conteúdo está organizado

```
📚 guia/        → Teoria explicada de forma clara, com exemplos de código
🧪 exercicios/  → Exercícios para praticar cada conceito
🚀 projetos/    → Projetos completos que aplicam tudo junto
```

---

## 🗺️ Roteiro sugerido

### Se você é INICIANTE (nunca programou em C#):
1. Leia o [Guia 01 — O que é POO?](01-o-que-e-poo.md)
2. Leia o [Guia 02 — Classes e Objetos](02-classes-e-objetos.md)
3. Faça os exercícios básicos 01 a 03
4. Continue pelos guias em ordem
5. A cada novo guia, faça os exercícios correspondentes
6. No final, implemente os projetos

### Se você já programa (intermediário):
1. Revise rapidamente os guias 01 a 03
2. Aprofunde nos guias 04 a 07 (os pilares)
3. Faça os exercícios intermediários
4. Implemente os projetos

### Se você quer revisar (avançado):
1. Consulte os guias como referência
2. Faça os exercícios avançados
3. Tente implementar os projetos sem olhar a solução

---

## 💡 Dicas de estudo

### ✅ O que fazer
- **Digite o código** — não copie e cole. Digitar ativa a memória muscular
- **Experimente** — mude os exemplos e veja o que acontece
- **Faça os exercícios antes de ver a solução**
- **Volte e revise** — releia os guias depois de fazer os exercícios
- **Construa os projetos do zero** — use o guia apenas quando travar

### ❌ O que evitar
- Não pule etapas — cada conceito depende do anterior
- Não fique travado por mais de 30 minutos sem pedir ajuda
- Não apenas leia — pratique sempre

---

## 🔧 Configurando o ambiente

### 1. Instale o .NET SDK

Acesse [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download) e instale a versão mais recente do .NET SDK.

Verifique a instalação:
```bash
dotnet --version
```

### 2. Escolha um editor

**VS Code** (gratuito, leve):
- Baixe em [https://code.visualstudio.com/](https://code.visualstudio.com/)
- Instale a extensão **C# Dev Kit**

**Visual Studio Community** (gratuito, completo):
- Baixe em [https://visualstudio.microsoft.com/](https://visualstudio.microsoft.com/)
- Selecione a carga de trabalho **.NET Desktop Development**

**JetBrains Rider** (pago, mas trial disponível):
- Ideal para projetos maiores

### 3. Criando um projeto para testar

```bash
# Criar uma pasta de testes
mkdir meus-testes && cd meus-testes

# Criar um projeto console
dotnet new console -n Testes

# Entrar na pasta do projeto
cd Testes

# Rodar o projeto
dotnet run
```

---

## 📁 Estrutura de cada exercício

Cada exercício tem:

```
exercicios/basico/ex01/
├── README.md       → Enunciado e instruções
├── Exercicio.cs    → Esqueleto do código (você vai preencher)
└── Solucao/
    └── Solucao.cs  → Solução completa (olhe só depois de tentar!)
```

---

## 📁 Estrutura de cada projeto

Cada projeto tem:

```
projetos/01-biblioteca-de-livros/
├── README.md           → Descrição e requisitos
├── PASSO-A-PASSO.md    → Guia de implementação
├── Program.cs          → Ponto de entrada
├── Models/             → Classes do domínio
├── Services/           → Lógica de negócio
└── .csproj             → Configuração do projeto
```

---

## ❓ Onde pedir ajuda

- **Abra uma Issue** neste repositório descrevendo sua dúvida
- Use o [Stack Overflow em português](https://pt.stackoverflow.com/)
- Entre em comunidades como o [Discord do .NET BR](https://discord.gg/dotnetbr)

---

Agora você está pronto! Vá para o [Guia 01 → O que é POO?](01-o-que-e-poo.md) e comece sua jornada. 🚀

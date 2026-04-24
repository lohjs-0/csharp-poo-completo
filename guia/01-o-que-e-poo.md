# 01 — O que é POO?

## O problema que a POO resolve

Imagine que você precisa gerenciar dados de alunos em um programa. Sem POO, você faria assim:

```csharp
// Jeito procedural (sem POO) — fica difícil de manter!
string aluno1Nome = "Ana";
int aluno1Idade = 20;
double aluno1Nota1 = 8.5;
double aluno1Nota2 = 7.0;

string aluno2Nome = "Carlos";
int aluno2Idade = 22;
double aluno2Nota1 = 9.0;
double aluno2Nota2 = 6.5;

// E se tiver 500 alunos? 😱
```

Com POO, você modela o mundo real com **objetos**:

```csharp
// Jeito orientado a objetos — muito mais organizado!
var aluno1 = new Aluno("Ana", 20);
aluno1.AdicionarNota(8.5);
aluno1.AdicionarNota(7.0);

var aluno2 = new Aluno("Carlos", 22);
aluno2.AdicionarNota(9.0);
aluno2.AdicionarNota(6.5);

Console.WriteLine(aluno1.CalcularMedia()); // 7.75
```

---

## O que é Programação Orientada a Objetos?

**POO** é um paradigma de programação que organiza o código em torno de **objetos** — estruturas que combinam **dados** (atributos) e **comportamentos** (métodos).

A ideia central é modelar o software da mesma forma que enxergamos o mundo real: com entidades que têm características e que fazem coisas.

---

## Os 4 Pilares da POO

A POO é construída sobre 4 princípios fundamentais:

### 1. 🔒 Encapsulamento
> Esconder os detalhes internos e expor apenas o necessário.

Um carro tem um motor complexo, mas você não precisa saber como ele funciona para dirigir — só precisa do volante e dos pedais. O motor está **encapsulado**.

### 2. 🧬 Herança
> Uma classe pode herdar características de outra.

Um `Cachorro` é um `Animal`. Em vez de reescrever tudo que um animal tem, a classe `Cachorro` herda de `Animal` e adiciona o que é específico dela.

### 3. 🎭 Polimorfismo
> O mesmo método pode ter comportamentos diferentes dependendo do objeto.

Tanto `Cachorro` quanto `Gato` herdam de `Animal` e têm o método `FazerSom()`. Mas o cachorro late e o gato mia — o mesmo método, comportamentos diferentes.

### 4. 🎨 Abstração
> Focar no que é essencial, ignorando detalhes irrelevantes.

Quando você usa um celular, você sabe que pode ligar, mandar mensagem e tirar foto. Você não precisa saber como os transistores funcionam. Isso é abstração.

---

## Classe vs Objeto

Esta é a distinção mais importante da POO:

| Conceito | Analogia | Exemplo |
|----------|----------|---------|
| **Classe** | Planta baixa / molde | `class Cachorro` |
| **Objeto** | A casa / o bolo | `var rex = new Cachorro()` |

Uma **classe** é a definição, o modelo. Um **objeto** é uma instância concreta dessa classe.

```csharp
// Classe = molde
class Cachorro
{
    public string Nome { get; set; }
    public string Raca { get; set; }

    public void Latir()
    {
        Console.WriteLine($"{Nome} diz: Au au!");
    }
}

// Objetos = instâncias do molde
var rex = new Cachorro { Nome = "Rex", Raca = "Pastor Alemão" };
var bolinha = new Cachorro { Nome = "Bolinha", Raca = "Poodle" };

rex.Latir();     // Rex diz: Au au!
bolinha.Latir(); // Bolinha diz: Au au!
```

`rex` e `bolinha` são dois objetos diferentes criados a partir do mesmo molde (`Cachorro`).

---

## Por que usar POO?

| Vantagem | Descrição |
|----------|-----------|
| **Organização** | Código agrupado por responsabilidade |
| **Reutilização** | Uma classe pode ser usada em vários lugares |
| **Manutenção** | Fácil de alterar sem quebrar o resto |
| **Escalabilidade** | Fácil de adicionar novas funcionalidades |
| **Modelagem** | Reflete o mundo real de forma natural |

---

## POO em C#

C# é uma linguagem **totalmente orientada a objetos**. Tudo em C# é uma classe — até os tipos primitivos como `int` e `string` são, internamente, classes.

```csharp
// Mesmo um "int" tem métodos!
int numero = 42;
string texto = numero.ToString(); // método do int!
int tamanho = texto.Length;       // propriedade da string!
```

---

## Resumo

- POO organiza o código em **objetos** que combinam dados e comportamentos
- A **classe** é o molde; o **objeto** é a instância
- Os 4 pilares são: **Encapsulamento, Herança, Polimorfismo e Abstração**
- C# é uma linguagem projetada para POO desde o início

---

## Próximo passo

Agora que você entende o conceito, vamos colocar a mão na massa!

➡️ [Guia 02 — Classes e Objetos](02-classes-e-objetos.md)

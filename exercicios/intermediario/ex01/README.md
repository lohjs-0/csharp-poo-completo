# Exercício Intermediário 01 — Herança: Animal

## 🎯 Objetivo
Praticar herança, override e o uso de `base`.

## 📋 Requisitos
Crie a hierarquia:
- `Animal` (base): Nome, Idade, Peso. Métodos: `FazerSom()` (virtual), `Comer()`, `Dormir()`, `toString()`
- `Cachorro : Animal`: Raça. Métodos: `FazerSom()` (late), `Buscar()`, `Abanar()`
- `Gato : Animal`: EhCastrado. Métodos: `FazerSom()` (mia), `Arranhar()`, `Ronronar()`
- `Passaro : Animal`: Envergadura. Métodos: `FazerSom()` (canta), `Voar()`, `Pousar()`

Crie uma lista `List<Animal>` e faça todos emitirem som com polimorfismo.

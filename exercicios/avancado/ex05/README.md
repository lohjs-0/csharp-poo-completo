# Exercício Avançado 05 — Singleton

## 🎯 Objetivo
Implementar o padrão Singleton de forma segura para multithreading.

## 📋 Requisitos
- `ConfiguracaoApp` singleton com propriedades de configuração
- `LoggerGlobal` singleton thread-safe com `Log()`, `LogErro()`, `LogInfo()`
- `CacheDistribuido` singleton que armazena dados com TTL (time-to-live)
- Demonstre que apenas uma instância é criada mesmo com múltiplas threads

## 💡 Conceitos
Singleton Pattern, Thread-safety, `Lazy<T>`, lock

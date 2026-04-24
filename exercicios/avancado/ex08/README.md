# Exercício Avançado 08 — Sistema de Eventos com Delegates

## 🎯 Objetivo
Usar delegates e eventos do C# para criar um sistema de eventos robusto.

## 📋 Requisitos
- `delegate void ManipuladorEvento<T>(object sender, T args)`
- Classe `Pedido` com eventos: `PedidoCriado`, `PedidoConfirmado`, `PedidoEnviado`, `PedidoCancelado`
- Handlers que reagem aos eventos: `LogHandler`, `EmailHandler`, `EstoqueHandler`, `FinanceiroHandler`
- Demonstre subscrição e cancelamento de eventos

## 💡 Conceitos
Delegates, Events, EventArgs, Padrão Event Handler

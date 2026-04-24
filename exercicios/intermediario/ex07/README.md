# Exercício Intermediário 07

## Polimorfismo com Notificações
Interface `INotificador` com `Enviar(string dest, string titulo, string corpo)`. Implemente: `EmailNotificador`, `SMSNotificador`, `PushNotificador`, `SlackNotificador`. Crie `NotificadorMulticanal` que dispara em todos os canais registrados e coleta resultado de cada um.

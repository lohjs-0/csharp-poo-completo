// Avançado 08 — Eventos com Delegates — SOLUÇÃO

class PedidoEventArgs : EventArgs
{
    public int PedidoId { get; init; }
    public string Cliente { get; init; } = "";
    public double Valor { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.Now;
}

class Pedido
{
    public int Id { get; }
    public string Cliente { get; }
    public double Valor { get; }
    public string Status { get; private set; } = "Criado";

    public event EventHandler<PedidoEventArgs>? PedidoCriado;
    public event EventHandler<PedidoEventArgs>? PedidoConfirmado;
    public event EventHandler<PedidoEventArgs>? PedidoEnviado;
    public event EventHandler<PedidoEventArgs>? PedidoCancelado;

    private static int _contador = 0;

    public Pedido(string cliente, double valor)
    {
        Id = ++_contador; Cliente = cliente; Valor = valor;
        PedidoCriado?.Invoke(this, new PedidoEventArgs { PedidoId = Id, Cliente = cliente, Valor = valor });
    }

    private PedidoEventArgs CriarArgs() => new() { PedidoId = Id, Cliente = Cliente, Valor = Valor };

    public void Confirmar() { Status = "Confirmado"; PedidoConfirmado?.Invoke(this, CriarArgs()); }
    public void Enviar() { Status = "Enviado"; PedidoEnviado?.Invoke(this, CriarArgs()); }
    public void Cancelar() { Status = "Cancelado"; PedidoCancelado?.Invoke(this, CriarArgs()); }
}

class Programa
{
    static void Main()
    {
        var pedido = new Pedido("Ana", 299.90);

        pedido.PedidoCriado += (s, e) => Console.WriteLine($"[LOG] Pedido #{e.PedidoId} criado para {e.Cliente}");
        pedido.PedidoConfirmado += (s, e) => Console.WriteLine($"[EMAIL] Confirmação enviada para {e.Cliente}");
        pedido.PedidoConfirmado += (s, e) => Console.WriteLine($"[ESTOQUE] Reservando itens do pedido #{e.PedidoId}");
        pedido.PedidoEnviado += (s, e) => Console.WriteLine($"[SMS] Pedido #{e.PedidoId} enviado!");
        pedido.PedidoCancelado += (s, e) => Console.WriteLine($"[FINANCEIRO] Estornando R${e.Valor:F2} para {e.Cliente}");

        Console.WriteLine("Criando pedido...");
        Console.WriteLine("Confirmando...");
        pedido.Confirmar();
        Console.WriteLine("Enviando...");
        pedido.Enviar();

        var pedido2 = new Pedido("Carlos", 150);
        pedido2.PedidoCriado += (s, e) => Console.WriteLine($"[LOG] Pedido #{e.PedidoId} criado");
        pedido2.PedidoCancelado += (s, e) => Console.WriteLine($"[LOG] Pedido #{e.PedidoId} cancelado");
        pedido2.Cancelar();
    }
}

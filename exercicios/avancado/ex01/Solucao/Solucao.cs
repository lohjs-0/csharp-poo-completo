// Avançado 01 — Sistema de Plugins — SOLUÇÃO

interface IPlugin
{
    string Nome { get; }
    string Versao { get; }
    void Inicializar();
    string Executar(string input);
    void Finalizar();
}

class PluginLogger : IPlugin
{
    private readonly List<string> _logs = new();
    public string Nome => "Logger";
    public string Versao => "1.0";

    public void Inicializar() => Console.WriteLine("[Logger] Inicializado.");
    public void Finalizar()
    {
        Console.WriteLine($"[Logger] Finalizando. {_logs.Count} entradas registradas.");
        foreach (var log in _logs) Console.WriteLine($"  >> {log}");
    }

    public string Executar(string input)
    {
        var entrada = $"[{DateTime.Now:HH:mm:ss}] {input}";
        _logs.Add(entrada);
        return input; // passa sem modificar
    }
}

class PluginMaiusculas : IPlugin
{
    public string Nome => "Maiúsculas";
    public string Versao => "1.0";
    public void Inicializar() => Console.WriteLine("[Maiúsculas] Inicializado.");
    public void Finalizar() => Console.WriteLine("[Maiúsculas] Finalizado.");
    public string Executar(string input) => input.ToUpper();
}

class PluginValidacao : IPlugin
{
    public string Nome => "Validação";
    public string Versao => "2.1";
    public void Inicializar() => Console.WriteLine("[Validação] Inicializado.");
    public void Finalizar() => Console.WriteLine("[Validação] Finalizado.");
    public string Executar(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new InvalidOperationException("Input vazio não é permitido.");
        return input.Trim();
    }
}

class PluginReverse : IPlugin
{
    public string Nome => "Reverse";
    public string Versao => "1.0";
    public void Inicializar() => Console.WriteLine("[Reverse] Inicializado.");
    public void Finalizar() => Console.WriteLine("[Reverse] Finalizado.");
    public string Executar(string input) => new string(input.Reverse().ToArray());
}

class GerenciadorPlugins
{
    private readonly List<IPlugin> _plugins = new();

    public void Registrar(IPlugin plugin)
    {
        _plugins.Add(plugin);
        Console.WriteLine($"Plugin '{plugin.Nome} v{plugin.Versao}' registrado.");
    }

    public void InicializarTodos() { foreach (var p in _plugins) p.Inicializar(); }
    public void FinalizarTodos() { foreach (var p in _plugins) p.Finalizar(); }

    public string Executar(string input)
    {
        string resultado = input;
        foreach (var plugin in _plugins)
        {
            Console.WriteLine($"  [{plugin.Nome}] entrada: '{resultado}'");
            resultado = plugin.Executar(resultado);
            Console.WriteLine($"  [{plugin.Nome}] saída:   '{resultado}'");
        }
        return resultado;
    }
}

class Programa
{
    static void Main()
    {
        var gerenciador = new GerenciadorPlugins();
        gerenciador.Registrar(new PluginValidacao());
        gerenciador.Registrar(new PluginMaiusculas());
        gerenciador.Registrar(new PluginLogger());
        gerenciador.Registrar(new PluginReverse());

        gerenciador.InicializarTodos();
        Console.WriteLine("\n=== Executando Pipeline ===");
        string resultado = gerenciador.Executar("  hello world  ");
        Console.WriteLine($"\nResultado final: '{resultado}'");

        Console.WriteLine("\n=== Finalizando ===");
        gerenciador.FinalizarTodos();
    }
}

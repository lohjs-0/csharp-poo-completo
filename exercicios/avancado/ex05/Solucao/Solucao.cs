// Avançado 05 — Singleton — SOLUÇÃO

// Singleton com Lazy<T> — thread-safe e lazy initialization
class ConfiguracaoApp
{
    private static readonly Lazy<ConfiguracaoApp> _instancia =
        new(() => new ConfiguracaoApp());

    public static ConfiguracaoApp Instancia => _instancia.Value;

    public string AppName { get; set; } = "MinhaApp";
    public string Versao { get; set; } = "1.0.0";
    public string Ambiente { get; set; } = "Producao";
    public int MaxConexoes { get; set; } = 10;

    private ConfiguracaoApp()
    {
        Console.WriteLine("ConfiguracaoApp criada (apenas uma vez!)");
    }
}

class LoggerGlobal
{
    private static readonly Lazy<LoggerGlobal> _instancia = new(() => new LoggerGlobal());
    public static LoggerGlobal Instancia => _instancia.Value;

    private readonly object _lock = new();
    private readonly List<string> _logs = new();

    private LoggerGlobal() { Console.WriteLine("Logger criado."); }

    public void Log(string mensagem)
    {
        lock (_lock)
        {
            var entrada = $"[{DateTime.Now:HH:mm:ss.fff}] {mensagem}";
            _logs.Add(entrada);
            Console.WriteLine(entrada);
        }
    }

    public void LogInfo(string msg) => Log($"[INFO] {msg}");
    public void LogErro(string msg) => Log($"[ERRO] {msg}");

    public IReadOnlyList<string> ObterLogs() => _logs.AsReadOnly();
}

class Programa
{
    static void Main()
    {
        // Demonstra que é sempre a mesma instância
        var config1 = ConfiguracaoApp.Instancia;
        var config2 = ConfiguracaoApp.Instancia;
        Console.WriteLine($"Mesma instância? {ReferenceEquals(config1, config2)}"); // True

        config1.AppName = "MeuSistema";
        Console.WriteLine($"config2.AppName = {config2.AppName}"); // MeuSistema

        var logger = LoggerGlobal.Instancia;
        logger.LogInfo("Sistema iniciado");
        logger.Log("Primeira ação");
        logger.LogErro("Teste de erro");

        Console.WriteLine($"\nTotal de logs: {logger.ObterLogs().Count}");
    }
}

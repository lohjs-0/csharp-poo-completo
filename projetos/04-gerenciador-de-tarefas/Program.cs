using Tarefas.Models;
using Tarefas.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("╔═════════════════════════════════╗");
Console.WriteLine("║   ✅ Gerenciador de Tarefas     ║");
Console.WriteLine("╚═════════════════════════════════╝\n");

var gerenciador = new GerenciadorTarefas();

// ── Criar tarefas ──
Console.WriteLine("=== Criando Tarefas ===");
var t1 = gerenciador.AdicionarTarefa(
    "Implementar autenticação JWT", "Criar sistema de login com tokens",
    Prioridade.Critica, "Backend", "Ana", DateTime.Now.AddDays(2));

var t2 = gerenciador.AdicionarTarefa(
    "Criar tela de login", "UI do formulário de login",
    Prioridade.Alta, "Frontend", "Carlos", DateTime.Now.AddDays(3));

var t3 = gerenciador.AdicionarTarefa(
    "Configurar CI/CD", "Pipeline de deploy automatizado",
    Prioridade.Media, "DevOps", "Ana", DateTime.Now.AddDays(7));

var t4 = gerenciador.AdicionarTarefa(
    "Escrever testes unitários", "Cobertura mínima de 80%",
    Prioridade.Alta, "Backend", "Maria", DateTime.Now.AddDays(5));

var t5 = gerenciador.AdicionarTarefa(
    "Documentar a API", "Swagger e README",
    Prioridade.Baixa, "Backend", "Carlos", null);

var t6 = gerenciador.AdicionarTarefa(
    "Revisar PR de segurança", "Code review crítico",
    Prioridade.Critica, "Backend", "Ana", DateTime.Now.AddDays(1));

// Adicionar tags
t1.AdicionarTag("segurança"); t1.AdicionarTag("autenticação");
t2.AdicionarTag("UI"); t2.AdicionarTag("login");
t4.AdicionarTag("testes"); t4.AdicionarTag("qualidade");

// ── Exibir todas ordenadas por prioridade ──
gerenciador.ExibirTodasTarefas("BACKLOG INICIAL");

// ── Simular progresso ──
Console.WriteLine("\n=== Iniciando Trabalho ===");
t1.IniciarProgresso();
t2.IniciarProgresso();
t6.IniciarProgresso();

Console.WriteLine("\n=== Concluindo Tarefas ===");
t1.Concluir();
t2.Concluir();
t5.Cancelar();

// ── Buscar tarefas de Ana ──
Console.WriteLine("\n=== Tarefas de Ana ===");
foreach (var t in gerenciador.BuscarPorResponsavel("Ana"))
    t.ExibirDetalhes();

// ── Tarefas de Backend ──
Console.WriteLine("\n=== Categoria: Backend ===");
foreach (var t in gerenciador.BuscarPorCategoria("Backend"))
    t.ExibirDetalhes();

// ── Filtro customizado com delegate ──
Console.WriteLine("\n=== Tarefas Críticas Ativas ===");
foreach (var t in gerenciador.Buscar(t => t.Prioridade == Prioridade.Critica && t.Ativa))
    t.ExibirDetalhes();

// ── Estado final ──
gerenciador.ExibirTodasTarefas("ESTADO ATUAL");

// ── Estatísticas ──
gerenciador.ExibirEstatisticas();

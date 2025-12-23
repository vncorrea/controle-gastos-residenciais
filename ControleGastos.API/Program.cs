using Microsoft.EntityFrameworkCore;
using ControleGastos.API.Data;
using ControleGastos.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração de serviços
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configurar serialização JSON para aceitar enums como números ou strings (ex: 1 ou "Despesa")
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        // Ignorar ciclos de referência circular (ex: Transacao -> Categoria -> Transacoes -> Categoria...)
        // Isso evita erro de serialização quando há relacionamentos bidirecionais
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do Entity Framework Core com SQLite
// A string de conexão aponta para um arquivo SQLite que persiste os dados mesmo após reiniciar o sistema
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "Data Source=controle-gastos.db"));

// Registro dos serviços de negócio (Dependency Injection)
builder.Services.AddScoped<IPessoaService, PessoaService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ITransacaoService, TransacaoService>();
builder.Services.AddScoped<IConsultaService, ConsultaService>();

// Configuração do CORS para permitir requisições do frontend React
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3000", 
                "http://localhost:5173",
                "https://localhost:3000",
                "https://localhost:5173"
            ) // Portas comuns do React/Vite
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configuração do pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Controle de Gastos API V1");
        c.RoutePrefix = "swagger"; // Swagger estará disponível em /swagger
    });
}

app.UseHttpsRedirection();

// Aplicar CORS
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

// Garantir que o banco de dados seja criado na primeira execução
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();

using ApiLocadora.DataContexts;
using ApiLocadora.Models;
using ApiLocadora.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Config connection database
var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<AppDbContext>(options => 
options
.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
.UseSnakeCaseNamingConvention()
);

builder.Services.AddScoped<ClientePfService>();
builder.Services.AddScoped<ClientePjService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<EnderecoContatoService>();
builder.Services.AddScoped<DespesaService>();
builder.Services.AddScoped<EstoqueService>();
builder.Services.AddScoped<FornecedorPfService>();
builder.Services.AddScoped<FornecedorPjService>();
builder.Services.AddScoped<FuncionarioService>();
builder.Services.AddScoped<LoginExclusivoService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

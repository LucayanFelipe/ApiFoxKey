using ApiLocadora.DataContexts;
using ApiLocadora.Models;
using ApiLocadora.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ApiFoxKey", Version = "v1" });

    // Mostra enums como string no Swagger UI
    c.UseInlineDefinitionsForEnums();
});

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
builder.Services.AddScoped<RelatorioCompraService>();
builder.Services.AddScoped<RelatorioCaixaService>();
builder.Services.AddScoped<RelatorioVendaService>();
builder.Services.AddScoped<MovimentacaoCaixaService>();
builder.Services.AddScoped<CompraService>();
builder.Services.AddScoped<CompraItemService>();
builder.Services.AddScoped<CaixaService>();
builder.Services.AddScoped<VendaService>();
builder.Services.AddScoped<PagamentoService>();
builder.Services.AddScoped<ParcelaService>();
builder.Services.AddScoped<NotaFiscalService>();
builder.Services.AddScoped<ItemVendaService>();

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

using Microsoft.EntityFrameworkCore;
using ApiLocadora.Models;

namespace ApiLocadora.DataContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<LoginExclusivo> loginsexclusivos { get; set; }
        public DbSet<Despesa> despesas { get; set; }
        public DbSet<EnderecoContato> enderecocontatos { get; set; }
        public DbSet<FornecedorPf> fornecedorpf { get; set; }
        public DbSet<FornecedorPj> fornecedorpj { get; set; }
        public DbSet<ClientePf> clientepf { get; set; }
        public DbSet<ClientePj> clientepj { get; set; }
        public DbSet<Funcionario> funcionarios { get; set; }
        public DbSet<Categoria> categorias { get; set; }
        public DbSet<Produto> produtos { get; set; }
        public DbSet<Estoque> estoques { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Caso precise configurar chaves compostas, nomes de tabelas ou relacionamentos, use o método OnModelCreating:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Exemplo de configuração para nomes de tabela se quiser:
            modelBuilder.Entity<Usuario>().ToTable("usuario");
            modelBuilder.Entity<LoginExclusivo>().ToTable("loginExclusivo");
            modelBuilder.Entity<Despesa>().ToTable("despesa");
            modelBuilder.Entity<EnderecoContato>().ToTable("endereco_contato");
            modelBuilder.Entity<FornecedorPf>().ToTable("fornecedor_pf");
            modelBuilder.Entity<FornecedorPj>().ToTable("fornecedor_pj");
            modelBuilder.Entity<ClientePf>().ToTable("cliente_pf");
            modelBuilder.Entity<ClientePj>().ToTable("cliente_pj");
            modelBuilder.Entity<Funcionario>().ToTable("funcionario");
            modelBuilder.Entity<Categoria>().ToTable("categoria");
            modelBuilder.Entity<Produto>().ToTable("produto");
            modelBuilder.Entity<Estoque>().ToTable("estoque");

            // Configurações adicionais podem ser feitas aqui, se necessário
        }
    }
}

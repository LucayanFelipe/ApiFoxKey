using Microsoft.EntityFrameworkCore;
using ApiLocadora.Models;

namespace ApiLocadora.DataContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<LoginExclusivo> LoginExclusivos { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<EnderecoContato> EnderecoContatos { get; set; }
        public DbSet<FornecedorPf> FornecedorPfs { get; set; }
        public DbSet<FornecedorPj> FornecedorPjs { get; set; }
        public DbSet<ClientePf> ClientePfs { get; set; }
        public DbSet<ClientePj> ClientePjs { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<MovimentacaoCaixa> MovimentacaoCaixas { get; set; }
        public DbSet<Caixa> Caixas { get; set; }
        public DbSet<RelatorioVenda> RelatorioVendas { get; set; }
        public DbSet<RelatorioCompra> RelatorioCompras { get; set; }
        public DbSet<RelatorioCaixa> RelatorioCaixas { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<CompraItem> CompraItens { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }
        public DbSet<NotaFiscal> NotasFiscais { get; set; }
        public DbSet<ItemVenda> ItensVendas { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Usuario>()
            .Property(u => u.PerfilAcesso)
            .HasConversion<string>(); // Salva o enum como string no banco

            modelBuilder.Entity<Usuario>().ToTable("usuario");
            modelBuilder.Entity<LoginExclusivo>().ToTable("login_exclusivo");
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
            modelBuilder.Entity<Caixa>().ToTable("caixa");
            modelBuilder.Entity<MovimentacaoCaixa>().ToTable("movimentacao_caixa");
            modelBuilder.Entity<RelatorioVenda>().ToTable("relatorio_venda");
            modelBuilder.Entity<RelatorioCompra>().ToTable("relatorio_compra");
            modelBuilder.Entity<RelatorioCaixa>().ToTable("relatorio_caixa");
            modelBuilder.Entity<Compra>().ToTable("compra");
            modelBuilder.Entity<CompraItem>().ToTable("compra_item");
            modelBuilder.Entity<Venda>().ToTable("venda");
            modelBuilder.Entity<Pagamento>().ToTable("pagamento");
            modelBuilder.Entity<Parcela>().ToTable("parcela");
            modelBuilder.Entity<NotaFiscal>().ToTable("nota_fiscal");
            modelBuilder.Entity<ItemVenda>().ToTable("item_venda");

            modelBuilder.Entity<Estoque>()
    .HasOne(e => e.Produto)
    .WithMany() // ou .WithMany(p => p.Estoques) se tiver coleção em Produto
    .HasForeignKey(e => e.Id_produto_fk)
    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

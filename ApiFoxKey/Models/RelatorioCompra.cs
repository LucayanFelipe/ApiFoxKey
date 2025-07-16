using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Models
{
    public class RelatorioCompra
    {
        [Key]
        public int Id_relatorio_compra { get; set; }
        public DateTime Data_inicial { get; set; }
        public DateTime Data_final { get; set; }
        public decimal Total_compras { get; set; }
        public int Qtd_compras { get; set; }
        public DateTime Data_gerada { get; set; }
        public string Produto_frequente { get; set; }
        public string Fornecedor_frequente { get; set; }
    }
}

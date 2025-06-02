using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Models
{
    public class RelatorioVenda
    {
        [Key]
        public int Id_relatorio_venda { get; set; }
        public DateTime Data_cadastro { get; set; }
        public decimal Total_vendas { get; set; }
        public decimal Total_recibo { get; set; }

    }
}

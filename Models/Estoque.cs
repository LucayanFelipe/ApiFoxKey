using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    public class Estoque
    {
        [Key]
        public int Id_estoque { get; set; }
        public decimal Qtd_atual { get; set; }
        public decimal? Qtd_reservada { get; set; }
        public decimal? Qtd_minima { get; set; }
        public string Status_estoque { get; set; }
        public string? Observacao { get; set; }
        public int? Id_produto_fk { get; set; }

        [ForeignKey("Id_produto_fk")]
        public Produto? Produto { get; set; }
    }
}

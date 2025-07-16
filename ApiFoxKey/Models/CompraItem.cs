using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    public class CompraItem
    {
        [Key]
        public int Id_compra_item { get; set; }

        public int Id_compra_fk { get; set; }

        public int Id_produto_fk { get; set; }

        public int Quantidade { get; set; }

        public decimal Preco_unitario { get; set; }
        public decimal Subtotal { get; set; }

        [ForeignKey("Id_compra_fk")]
        public Compra Compra { get; set; }

        [ForeignKey("Id_produto_fk")]
        public Produto Produto { get; set; }
    }
}

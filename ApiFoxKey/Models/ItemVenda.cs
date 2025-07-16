using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    public class ItemVenda
    {
        [Key]
        public int Id_item_venda { get; set; }

        public int Qtd { get; set; }

        public decimal? Preco_unit { get; set; }

        public decimal? Subtotal { get; set; }

        public int Id_venda_fk { get; set; }


        public int Id_produto_fk { get; set; }


        [ForeignKey("Id_venda_fk")]
        public Venda Venda { get; set; }


        [ForeignKey("Id_produto_fk")]
        public Produto Produto { get; set; }
    }
}

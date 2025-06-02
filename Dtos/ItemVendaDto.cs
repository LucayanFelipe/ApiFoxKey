using System;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class ItemVendaDto
    {
        [Required]
        public int Qtd { get; set; }

        public decimal? Preco_unit { get; set; }

        [Required]
        public int Id_venda_fk { get; set; }

        [Required]
        public int Id_produto_fk { get; set; }
    }
}

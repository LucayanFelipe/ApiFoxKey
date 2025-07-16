using System;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class ItemVendaDto
    {
        [Required]
        public required int Qtd { get; set; }

        [Required]
        public required decimal? Preco_unit { get; set; }

        [Required]
        public required int Id_venda_fk { get; set; }

        [Required]
        public required int Id_produto_fk { get; set; }
    }
}

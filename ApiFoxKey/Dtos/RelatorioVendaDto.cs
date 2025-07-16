using System;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class RelatorioVendaDto
    {
        [Required]
        public DateTime Data_cadastro { get; set; }

        [Required]
        public decimal Total_vendas { get; set; }

        [Required]
        public decimal Total_recibo { get; set; }
    }
}

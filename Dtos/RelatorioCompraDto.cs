using System;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class RelatorioCompraDto
    {
        [Required]
        public DateTime Data_inicial { get; set; }

        [Required]
        public DateTime Data_final { get; set; }

        [Required]
        public decimal Total_compras { get; set; }

        [Required]
        public int Qtd_compras { get; set; }

        [Required]
        public DateTime Data_gerada { get; set; }

        [Required]
        public string Produto_frequente { get; set; }

        [Required]
        public string Fornecedor_frequente { get; set; }
    }
}

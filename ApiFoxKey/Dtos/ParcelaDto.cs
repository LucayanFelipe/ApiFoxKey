using System;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class ParcelaDto
    {
        [Required]
        public int Qtd_parcelas { get; set; }

        [Required]
        public DateTime Data_vencimento { get; set; }

        [Required]
        public decimal Valor_parcela { get; set; }

        [Required]
        public string Status_parcela { get; set; }

        [Required]
        public DateTime Data_pagamento { get; set; }

        [Required]
        public int Id_pagamento_fk { get; set; }

        [Required]
        public int Id_despesa_fk { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    public class Parcela
    {
        [Key]
        public int Id_parcela { get; set; }
        public int Qtd_parcelas { get; set; }
        public DateTime Data_vencimento { get; set; }


        public decimal Valor_parcela { get; set; }


        public string Status_parcela { get; set; }


        public DateTime Data_pagamento { get; set; }


        public int Id_pagamento_fk { get; set; }


        public int Id_despesa_fk { get; set; }

        [ForeignKey("Id_pagamento_fk")]
        public Pagamento Pagamento { get; set; }

        [ForeignKey("Id_despesa_fk")]
        public Despesa Despesa { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    public class Pagamento
    {
        [Key]
        public int Id_pagamento { get; set; }

        public decimal Valor_pago { get; set; }

        public DateTime Data_pagamento { get; set; }


        public bool Parcelado { get; set; }

        public int? Qtd_parcelas { get; set; }


        public string Status_pagamento { get; set; }

        public int Id_venda_fk { get; set; }

        public int? Id_cliente_pf_fk { get; set; }

        public int? Id_cliente_pj_fk { get; set; }

        [ForeignKey("Id_venda_fk")]
        public Venda Venda { get; set; }

        [ForeignKey("Id_cliente_pf_fk")]
        public ClientePf ClientePf { get; set; }

        [ForeignKey("Id_cliente_pj_fk")]
        public ClientePj ClientePj { get; set; }
    }
}

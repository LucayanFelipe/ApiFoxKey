using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    public class Venda
    {
        [Key]
        public int Id_venda { get; set; }


        public DateTime Data_gerada { get; set; }


        public TimeSpan Hora { get; set; }


        public decimal Valor_total { get; set; }

        public decimal? Desconto { get; set; }


        public decimal Valor_final { get; set; }


        public string Forma_pagamento { get; set; }


        public string Status_venda { get; set; }


        public int Id_caixa_fk { get; set; }

        public int? Id_cliente_pf_fk { get; set; }

        public int? Id_cliente_pj_fk { get; set; }



        [ForeignKey("Id_caixa_fk")]
        public Caixa Caixa { get; set; }


        [ForeignKey("Id_cliente_pf_fk")]
        public ClientePf ClientePf { get; set; }


        [ForeignKey("Id_cliente_pj_fk")]
        public ClientePj ClientePj { get; set; }
    }
}

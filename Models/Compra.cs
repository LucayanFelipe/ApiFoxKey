using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    public class Compra
    {
        [Key]
        public int Id_compra { get; set; }

        public DateTime Data_compra { get; set; }

        public decimal Valor_total { get; set; }

        public string Tipo_pagamento { get; set; }

        public string Observacao { get; set; }
        public string Status_compra { get; set; }

        public int? Id_fornecedor_pf_fk { get; set; }

        public int? Id_fornecedor_pj_fk { get; set; }

        public int Id_login_fk { get; set; }

        [ForeignKey("Id_fornecedor_pf_fk")]
        public FornecedorPf FornecedorPf { get; set; }

        [ForeignKey("Id_fornecedor_pj_fk")]
        public FornecedorPj FornecedorPj { get; set; }

        [ForeignKey("Id_login_fk")]
        public LoginExclusivo LoginExclusivo { get; set; }
    }
}

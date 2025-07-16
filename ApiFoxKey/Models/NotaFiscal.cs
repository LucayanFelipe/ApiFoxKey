using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    public class NotaFiscal
    {
        [Key]
        public int Id_nota_fiscal { get; set; }

        public string Numero { get; set; }


        public DateTime Data_emissao { get; set; }


        public decimal Valor_total { get; set; }

        public string Tipo { get; set; }


        public string Chave_acesso { get; set; }

        public string? Xml_nota { get; set; }


        public int Id_venda_fk { get; set; }


        [ForeignKey("Id_venda_fk")]
        public Venda Venda { get; set; }
    }
}

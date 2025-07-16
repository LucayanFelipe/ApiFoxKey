using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    public class RelatorioCaixa
    {
        [Key]
        public int Id_relatorio_caixa { get; set; }

        public DateTime Data_abertura { get; set; }

        public DateTime Data_fechamento { get; set; }

        public string Operador { get; set; }

        public decimal Saldo_inicial { get; set; }

        public decimal Entrada_vendas { get; set; }

        public decimal Entrada_reforco { get; set; }

        public decimal Total_entradas { get; set; }

        public decimal Saida_sangria { get; set; }

        public decimal Saida_despesa { get; set; }

        public decimal Total_saida { get; set; }

        public decimal Saldo_final { get; set; }


        public string Observacoes { get; set; }

        public DateTime Data_gerada { get; set; }


        public int? Id_caixa_fk { get; set; }

        [ForeignKey("Id_caixa_fk")]
        public Caixa Caixa { get; set; }
    }
}

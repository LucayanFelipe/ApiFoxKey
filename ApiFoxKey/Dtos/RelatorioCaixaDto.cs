using System;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class RelatorioCaixaDto
    {
        [Required]
        public DateTime Data_abertura { get; set; }

        [Required]
        public DateTime Data_fechamento { get; set; }

        [Required]
        public string Operador { get; set; }

        [Required]
        public decimal Saldo_inicial { get; set; }

        [Required]
        public decimal Entrada_vendas { get; set; }

        [Required]
        public decimal Entrada_reforco { get; set; }

        [Required]
        public decimal Total_entradas { get; set; }

        [Required]
        public decimal Saida_sangria { get; set; }

        [Required]
        public decimal Saida_despesa { get; set; }

        [Required]
        public decimal Total_saida { get; set; }

        [Required]
        public decimal Saldo_final { get; set; }

        [Required]
        public string Observacoes { get; set; }

        [Required]
        public DateTime Data_gerada { get; set; }

        [Required]
        public int? Id_caixa_fk { get; set; }
    }
}

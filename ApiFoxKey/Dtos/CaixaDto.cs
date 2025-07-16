using System;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class CaixaDto
    {
        [Required]
        public DateTime Data_abertura { get; set; }

        [Required]
        public DateTime Data_fechamento { get; set; }

        [Required]
        public decimal Saldo_inicial { get; set; }

        [Required]
        public decimal Saldo_final { get; set; }

        [Required]
        public decimal Total_entrada { get; set; }

        [Required]
        public int Id_funcionario_fk { get; set; }

        [Required]
        public int Id_login_fk { get; set; }

        [Required]
        public int Id_movimentacao_fk { get; set; }
    }
}

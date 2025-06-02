using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Models
{
    public class Caixa
    {
        [Key]
        public int Id_caixa { get; set; }
        public DateTime Data_abertura { get; set; }
        public DateTime Data_fechamento { get; set; }
        public decimal Saldo_inicial { get; set; }
        public decimal Saldo_final { get; set; }
        public decimal Total_entrada { get; set; }
        public int Id_funcionario_fk { get; set; }
        public int Id_login_fk { get; set; }
        public int Id_movimentacao_fk { get; set; }

        [ForeignKey("Id_funcionario_fk")]
        public Funcionario Funcionario { get; set; }

        [ForeignKey("Id_login_fk")]
        public LoginExclusivo LoginExclusivo { get; set; }

        [ForeignKey("Id_movimentacao_fk")]
        public MovimentacaoCaixa MovimentacaoCaixa { get; set; }
    }
}

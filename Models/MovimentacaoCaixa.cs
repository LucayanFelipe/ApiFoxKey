using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Models
{
    public class MovimentacaoCaixa
    {
        [Key]
        public int Id_movimentacao_caixa { get; set; }
        public string Tipo { get; set; }

        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public DateTime? Data_gerada { get; set; }
        public TimeOnly Hora { get; set; }

    }

}

using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class MovimentacaoCaixaDto
    {
        [Required]
        public string Tipo { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public DateTime? Data_gerada { get; set; }

        [Required]
        public TimeOnly Hora { get; set; }
    }
}

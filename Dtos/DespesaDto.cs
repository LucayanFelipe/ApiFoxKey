using ApiLocadora.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class DespesaDto
    {
        [Required]
        public required string Tipo_despesa { get; set; }

        [Required]
        public required decimal Valor { get; set; }

        [Required]
        public required DateTime Data_gerada { get; set; }

        [Required]
        public required string Descricao { get; set; }

        [Required]
        public required int Id_login_fk { get; set; }
    }
}

using ApiLocadora.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class EstoqueDto
    {
        [Required]
        public required decimal Qtd_atual { get; set; }

        [Required]
        public required decimal Qtd_reservada { get; set; }

        [Required]
        public required decimal Qtd_minima { get; set; }

        [Required]
        public required string Status_estoque { get; set; }

        [Required]
        public required string Observacao { get; set; }

        [Required]
        public required int Id_produto_fk { get; set; }
    }
}

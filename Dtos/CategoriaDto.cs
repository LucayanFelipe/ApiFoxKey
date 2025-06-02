using ApiLocadora.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class CategoriaDto
    {
        [Required]
        public required string Nome { get; set; }

        [Required]
        public required string Descricao { get; set; }

        [Required]
        public required int Prioridade_reposicao { get; set; }

        [Required]
        public required DateTime Data_registro { get; set; }

        [Required]
        public required bool Ativo { get; set; }
    }
}

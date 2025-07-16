using ApiLocadora.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class FornecedorPfDto
    {
        [Required]
        public required string Nome { get; set; }

        [Required]
        public required string Sobrenome { get; set; }

        [Required]
        public required string Cpf { get; set; }

        [Required]
        public required DateTime Data_nascimento { get; set; }

        [Required]
        public required string Rg { get; set; }

        [Required]
        public required string Sexo { get; set; }

        [Required]
        public required string Estado_civil { get; set; }

        [Required]
        public required string Orgao_expedidor { get; set; }

        [Required]
        public required string Nacionalidade { get; set; }

        [Required]
        public required string Raca { get; set; }

        [Required]
        public required EnderecoContatoDto EnderecoContato { get; set; }
    }
}

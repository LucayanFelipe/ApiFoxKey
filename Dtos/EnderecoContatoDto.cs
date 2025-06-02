using ApiLocadora.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class EnderecoContatoDto
    {
        [Required]
        public required string Rua { get; set; }

        [Required]
        public required string Numero { get; set; }

        [Required]
        public required string Bairro { get; set; }


        public required string Complemento { get; set; }


        public required string Referencia { get; set; }

        [Required]
        public required string Cep { get; set; }

        [Required]
        public required string Estado { get; set; }

        [Required]
        public required string Cidade { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Celular { get; set; }
    }
}

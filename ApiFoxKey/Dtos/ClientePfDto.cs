using ApiLocadora.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class ClientePfDto
    {
        [Required]
        public required string Nome { get; set; }

        [Required]
        public required string Sobrenome { get; set; }

        [Required]
        public required DateTime Data_nascimento { get; set; }

        [Required]
        public required string Cpf { get; set; }

        [Required]
        public required string Rg { get; set; }

        [Required]
        public required string Sexo { get; set; }

        [Required]
        public required int Id_endereco_contato_fk { get; set; }
    }
}

using ApiLocadora.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class FuncionarioDto
    {
        [Required]
        public required string Nome { get; set; }

        [Required]
        public required string Sobrenome { get; set; }

        [Required]
        public required string Cpf { get; set; }

        [Required]
        public required string Rg { get; set; }

        [Required]
        public required string Orgao_expedidor { get; set; }

        [Required]
        public required string Nacionalidade { get; set; }

        [Required]
        public required string Numero_ctps { get; set; }

        [Required]
        public required string Numero_pis { get; set; }

        [Required]
        public required string Raca { get; set; }

        [Required]
        public required string Sexo { get; set; }

        [Required]
        public required string Estado_civil { get; set; }

        [Required]
        public required string Cargo { get; set; }

        [Required]
        public required string Grau_instrucao { get; set; }

        [Required]
        public required DateTime Data_nascimento { get; set; }

        [Required]
        public required int Id_endereco_contato_fk { get; set; }
    }
}

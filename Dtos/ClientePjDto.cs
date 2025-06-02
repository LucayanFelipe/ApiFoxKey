using ApiLocadora.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class ClientePjDto
    {
        [Required]
        public required string Nome_fantasia { get; set; }

        [Required]
        public required string Razao_social { get; set; }

        [Required]
        public required string Inscricao_municipal { get; set; }

        [Required]
        public required string Cnpj { get; set; }

        [Required]
        public required DateTime Data_abertura { get; set; }

        [Required]
        public required string Representante { get; set; }

        [Required]
        public required int Id_endereco_contato_fk { get; set; }
    }
}

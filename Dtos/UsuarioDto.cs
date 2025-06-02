using ApiLocadora.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiLocadora.Dtos
{
    public class UsuarioDto
    {
        [Required]
        public required string Nome { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Senha { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PerfilAcesso Perfil_acesso { get; set; }
    }
}

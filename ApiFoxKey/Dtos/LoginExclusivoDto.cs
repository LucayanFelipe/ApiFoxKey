using ApiLocadora.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class LoginExclusivoDto
    {
        [Required]
        public required DateTime Data_ativacao { get; set; }

        [Required]
        public required int Id_usuario_fk { get; set; }
    }
}

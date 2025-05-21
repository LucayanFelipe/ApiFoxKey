using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Models
{
    public class LoginExclusivo
    {
        [Key]
        public int IdLogin { get; set; }
        public DateTime Data_ativacao { get; set; }

        public int Id_usuario_fk { get; set; }
        public Usuario Usuario { get; set; }
    }

}

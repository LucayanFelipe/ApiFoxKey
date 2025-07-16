using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    public class LoginExclusivo
    {
        [Key]
        public int Id_login { get; set; }
        public DateTime Data_ativacao { get; set; }

        public int Id_usuario_fk { get; set; }


        [ForeignKey("Id_usuario_fk")]
        public Usuario Usuario { get; set; }
    }

}

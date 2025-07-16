using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    public class Despesa
    {
        [Key]
        public int Id_despesa { get; set; }
        public string Tipo_despesa { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data_gerada { get; set; }
        public string Descricao { get; set; }

        public int? Id_login_fk { get; set; }

        [ForeignKey("Id_login_fk")]
        public LoginExclusivo Login_exclusivo { get; set; }
    }
}
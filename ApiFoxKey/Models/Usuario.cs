using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Models
{
    public enum PerfilAcesso
    {
        ADMIN,
        GERENTE,
        VENDEDOR,
        CAIXA
    }
    public class Usuario
    {
        [Key]
        public int Id_usuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public PerfilAcesso PerfilAcesso { get; set; }
    }
}

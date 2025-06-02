using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Models
{
    public class ClientePf
    {
        [Key]
        public int Id_cliente_pf { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime Data_nascimento { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Sexo { get; set; }
        public int Id_endereco_contato_fk { get; set; }
        [ForeignKey("Id_endereco_contato_fk")]
        public EnderecoContato EnderecoContato { get; set; }
    }
}

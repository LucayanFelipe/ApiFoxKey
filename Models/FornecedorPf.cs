using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLocadora.Models
{
    public class FornecedorPf
    {
        [Key]
        public int Id_fornecedor_pf { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public DateTime Data_nascimento { get; set; }
        public string Rg { get; set; }
        public string Sexo { get; set; }
        public string Estado_civil { get; set; }
        public string Orgao_expedidor { get; set; }
        public string Nacionalidade { get; set; }
        public string Raca { get; set; }
        public int Id_endereco_contato_fk { get; set; }

        [ForeignKey("Id_endereco_contato_fk")]
        public EnderecoContato EnderecoContato { get; set; }
    }
}

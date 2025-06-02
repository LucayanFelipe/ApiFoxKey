using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Models
{
    public class EnderecoContato
    {
        [Key]
        public int Id_endereco_contato { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string? Complemento { get; set; }
        public string? Referencia { get; set; }
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Models
{
    public class FornecedorPj
    {
        [Key]
        public int Id_fornecedor_pj { get; set; }
        public string Nome_fantasia { get; set; }
        public string? Razao_social { get; set; }
        public string? Inscricao_municipal { get; set; }
        public string Cnpj { get; set; }
        public DateTime? Data_abertura { get; set; }
        public string Representante { get; set; }
        public int Id_endereco_contato_fk { get; set; }
    }
}

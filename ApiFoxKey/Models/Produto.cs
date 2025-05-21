using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Models
{
    public class Produto
    {
        [Key]
        public int Id_produto { get; set; }
        public string? Codigo_barra { get; set; }
        public string? Unidade_medida { get; set; }
        public decimal? Preco_custo { get; set; }
        public decimal? Preco_venda { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? Data_cadastro { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int Id_categoria_fk { get; set; }
        public int Id_fornecedor_pf_fk { get; set; }
        public int Id_fornecedor_pj_fk { get; set; }
    }
}

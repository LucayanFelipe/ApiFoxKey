using ApiLocadora.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class ProdutoDto
    {
        [Required]
        public required string Codigo_barra { get; set; }

        [Required]
        public required string Unidade_medida { get; set; }

        [Required]
        public required decimal Preco_custo { get; set; }

        [Required]
        public required decimal Preco_venda { get; set; }

        [Required]
        public required bool Ativo { get; set; }

        [Required]
        public required DateTime Data_cadastro { get; set; }

        [Required]
        public required string Nome { get; set; }

        [Required]
        public required string Descricao { get; set; }

        [Required]
        public required int Id_categoria_fk { get; set; }

        [Required]
        public required int Id_fornecedor_pf_fk { get; set; }

        [Required]
        public required int Id_fornecedor_pj_fk { get; set; }
    }
}

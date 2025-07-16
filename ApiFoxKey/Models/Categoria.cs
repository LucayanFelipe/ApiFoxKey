using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Models
{
    public class Categoria
    {
        [Key]
        public int Id_categoria { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public int Prioridade_reposicao { get; set; }
        public DateTime Data_registro { get; set; }
        public bool Ativo { get; set; }
    }
}

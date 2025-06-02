using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class CompraItemDto
    {
        [Required]
        public int Id_compra_fk { get; set; }

        [Required]
        public int Id_produto_fk { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public decimal Preco_unitario { get; set; }

        // Subtotal REMOVIDO, pois é gerado automaticamente pelo banco.
    }
}

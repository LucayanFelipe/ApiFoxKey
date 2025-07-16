using System.ComponentModel.DataAnnotations;


namespace ApiLocadora.Dtos
{
    public class ItemVendaDetalhadaDto
    {
        public int Id_item_venda { get; set; }
        public int Qtd { get; set; }
        public decimal? Preco_unit { get; set; }
        public decimal? Subtotal { get; set; }
        public string NomeProduto { get; set; }
    }

}
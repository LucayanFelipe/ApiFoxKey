using ApiLocadora.Dtos;
namespace ApiLocadora.Dtos
{
    public class VendaDetailsDto
    {
        public int Id_venda { get; set; }
        public DateTime Data_gerada { get; set; }
        public decimal Valor_total { get; set; }
        public decimal Valor_final { get; set; }
        public decimal? Desconto { get; set; }
        public string Forma_pagamento { get; set; }
        public string Status_venda { get; set; }

        public string NomeClientePf { get; set; }
        public string NomeClientePj { get; set; }

        public List<ItemVendaDetalhadaDto> ItensVenda { get; set; }

    }
}
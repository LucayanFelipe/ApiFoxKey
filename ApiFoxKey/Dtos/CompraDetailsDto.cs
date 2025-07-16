namespace ApiLocadora.Dtos
{
    public class CompraDetailsDto
    {
        public int Id_compra { get; set; }
        public DateTime Data_compra { get; set; }
        public decimal Valor_total { get; set; }
        public string Tipo_pagamento { get; set; }
        public string Observacao { get; set; }
        public string Status_compra { get; set; }

        public string NomeFornecedorPf { get; set; }
        public string NomeFornecedorPj { get; set; }
        public List<ItemCompraDetalhadaDto> ItensCompra { get; set; }
    }

}
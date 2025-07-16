using System;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class NotaFiscalDto
    {
        [Required]
        public string Numero { get; set; }

        [Required]
        public DateTime Data_emissao { get; set; }

        [Required]
        public decimal Valor_total { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Required]
        public string Chave_acesso { get; set; }

        [Required]
        public string? Xml_nota { get; set; }

        [Required]
        public int Id_venda_fk { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class VendaDto : IValidatableObject
    {
        [Required]
        public DateTime Data_gerada { get; set; }

        [Required]
        public TimeSpan Hora { get; set; }

        [Required]
        public decimal Valor_total { get; set; }

        public decimal? Desconto { get; set; }

        [Required]
        public decimal Valor_final { get; set; }

        [Required]
        public string Forma_pagamento { get; set; }

        [Required]
        public string Status_venda { get; set; }

        [Required]
        public int? Id_caixa_fk { get; set; }

        public int? Id_cliente_pf_fk { get; set; }

        public int? Id_cliente_pj_fk { get; set; }

        // Validação customizada para garantir a regra do CHECK constraint do banco
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool pfPreenchido = Id_cliente_pf_fk.HasValue;
            bool pjPreenchido = Id_cliente_pj_fk.HasValue;

            if (!(pfPreenchido ^ pjPreenchido)) // XOR: apenas um deve ser verdadeiro
            {
                yield return new ValidationResult(
                    "Exatamente um dos campos Id_cliente_pf_fk ou Id_cliente_pj_fk deve ser preenchido.",
                    new[] { nameof(Id_cliente_pf_fk), nameof(Id_cliente_pj_fk) }
                );
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class PagamentoDto : IValidatableObject
    {
        [Required]
        public decimal Valor_pago { get; set; }

        [Required]
        public DateTime Data_pagamento { get; set; }

        [Required]
        public bool Parcelado { get; set; }

        public int? Qtd_parcelas { get; set; }

        [Required]
        public string Status_pagamento { get; set; }

        [Required]
        public int Id_venda_fk { get; set; }

        public int? Id_cliente_pf_fk { get; set; }

        public int? Id_cliente_pj_fk { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool pfPreenchido = Id_cliente_pf_fk.HasValue;
            bool pjPreenchido = Id_cliente_pj_fk.HasValue;

            if (!(pfPreenchido ^ pjPreenchido)) // XOR para garantir apenas um preenchido
            {
                yield return new ValidationResult(
                    "Exatamente um dos campos Id_cliente_pf_fk ou Id_cliente_pj_fk deve ser preenchido.",
                    new[] { nameof(Id_cliente_pf_fk), nameof(Id_cliente_pj_fk) }
                );
            }
        }
    }
}

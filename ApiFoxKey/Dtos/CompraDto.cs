using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Dtos
{
    public class CompraDto : IValidatableObject
    {
        [Required]
        public DateTime Data_compra { get; set; }

        [Required]
        public decimal Valor_total { get; set; }

        [Required]
        public string Tipo_pagamento { get; set; }

        [Required]
        public string Observacao { get; set; }

        [Required]
        [StringLength(20)]
        public string Status_compra { get; set; }

        public int? Id_fornecedor_pf_fk { get; set; }

        public int? Id_fornecedor_pj_fk { get; set; }

        [Required]
        public int Id_login_fk { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            bool pfPreenchido = Id_fornecedor_pf_fk.HasValue;
            bool pjPreenchido = Id_fornecedor_pj_fk.HasValue;

            if (!(pfPreenchido ^ pjPreenchido)) // XOR para garantir só um preenchido
            {
                yield return new ValidationResult(
                    "Exatamente um dos campos Id_fornecedor_pf_fk ou Id_fornecedor_pj_fk deve ser preenchido.",
                    new[] { nameof(Id_fornecedor_pf_fk), nameof(Id_fornecedor_pj_fk) }
                );
            }
        }
    }
}

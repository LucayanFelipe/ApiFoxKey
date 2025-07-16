using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLocadora.Services
{
    public class ParcelaService
    {
        private readonly AppDbContext _context;

        public ParcelaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Parcela>> GetAll()
        {
            return await _context.Parcelas
                .Include(p => p.Pagamento)
                .Include(p => p.Despesa)
                .ToListAsync();
        }

        public async Task<Parcela> GetOneById(int id)
        {
            try
            {
                var parcela = await _context.Parcelas
                    .Include(p => p.Pagamento)
                    .Include(p => p.Despesa)
                    .SingleOrDefaultAsync(x => x.Id_parcela == id);

                return parcela ?? throw new Exception("Parcela não encontrada.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Parcela> Create(ParcelaDto item)
        {
            try
            {
                var novaParcela = new Parcela
                {
                    Qtd_parcelas = item.Qtd_parcelas,
                    Data_vencimento = item.Data_vencimento,
                    Valor_parcela = item.Valor_parcela,
                    Status_parcela = item.Status_parcela,
                    Data_pagamento = item.Data_pagamento,
                    Id_pagamento_fk = item.Id_pagamento_fk,
                    Id_despesa_fk = item.Id_despesa_fk
                };

                await _context.Parcelas.AddAsync(novaParcela);
                await _context.SaveChangesAsync();

                return novaParcela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Parcela> Update(int id, ParcelaDto item)
        {
            try
            {
                var parcela = await _context.Parcelas.FirstOrDefaultAsync(x => x.Id_parcela == id);

                if (parcela == null)
                    throw new Exception("Parcela não encontrada.");

                parcela.Qtd_parcelas = item.Qtd_parcelas;
                parcela.Data_vencimento = item.Data_vencimento;
                parcela.Valor_parcela = item.Valor_parcela;
                parcela.Status_parcela = item.Status_parcela;
                parcela.Data_pagamento = item.Data_pagamento;
                parcela.Id_pagamento_fk = item.Id_pagamento_fk;
                parcela.Id_despesa_fk = item.Id_despesa_fk;

                await _context.SaveChangesAsync();

                return parcela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Parcela> Delete(int id)
        {
            try
            {
                var parcela = await _context.Parcelas.FirstOrDefaultAsync(x => x.Id_parcela == id);
                if (parcela == null)
                    return null;

                _context.Parcelas.Remove(parcela);
                await _context.SaveChangesAsync();

                return parcela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

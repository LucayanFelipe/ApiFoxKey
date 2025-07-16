using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class PagamentoService
    {
        private readonly AppDbContext _context;

        public PagamentoService(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<ICollection<Pagamento>> GetAll()
        {
            return await _context.Pagamentos
                .Include(p => p.Venda)
                .Include(p => p.ClientePf)
                .Include(p => p.ClientePj)
                .ToListAsync();
        }

        // GET ONE BY ID
        public async Task<Pagamento> GetOneById(int id)
        {
            var pagamento = await _context.Pagamentos
                .Include(p => p.Venda)
                .Include(p => p.ClientePf)
                .Include(p => p.ClientePj)
                .SingleOrDefaultAsync(p => p.Id_pagamento == id);

            return pagamento ?? throw new Exception("Pagamento não encontrado.");
        }

        // CREATE
        public async Task<Pagamento> Create(PagamentoDto item)
        {
            try
            {
                var novo = new Pagamento
                {
                    Valor_pago = item.Valor_pago,
                    Data_pagamento = item.Data_pagamento,
                    Parcelado = item.Parcelado,
                    Qtd_parcelas = item.Qtd_parcelas,
                    Status_pagamento = item.Status_pagamento,
                    Id_venda_fk = item.Id_venda_fk,
                    Id_cliente_pf_fk = item.Id_cliente_pf_fk,
                    Id_cliente_pj_fk = item.Id_cliente_pj_fk
                };

                await _context.Pagamentos.AddAsync(novo);
                await _context.SaveChangesAsync();

                return novo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // UPDATE
        public async Task<Pagamento> Update(int id, PagamentoDto item)
        {
            try
            {
                var pagamento = await _context.Pagamentos.FirstOrDefaultAsync(x => x.Id_pagamento == id);

                if (pagamento == null)
                    throw new Exception("Nota Fiscal não encontrada.");
                pagamento.Valor_pago = item.Valor_pago;
                pagamento.Data_pagamento = item.Data_pagamento;
                pagamento.Parcelado = item.Parcelado;
                pagamento.Qtd_parcelas = item.Qtd_parcelas;
                pagamento.Status_pagamento = item.Status_pagamento;
                pagamento.Id_venda_fk = item.Id_venda_fk;
                pagamento.Id_cliente_pf_fk = item.Id_cliente_pf_fk;
                pagamento.Id_cliente_pj_fk = item.Id_cliente_pj_fk;

                await _context.SaveChangesAsync();

                return pagamento;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE
        public async Task<Pagamento> Delete(int id)
        {
            var existente = await _context.Pagamentos.FindAsync(id);
            if (existente == null)
                throw new Exception("Pagamento não encontrado.");

            _context.Pagamentos.Remove(existente);
            await _context.SaveChangesAsync();
            return existente;
        }
    }
}

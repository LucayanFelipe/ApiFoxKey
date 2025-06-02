using ApiLocadora.DataContexts;
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
        public async Task<Pagamento> Create(Pagamento pagamento)
        {
            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();
            return pagamento;
        }

        // UPDATE
        public async Task<Pagamento> Update(int id, Pagamento pagamentoAtualizado)
        {
            var existente = await _context.Pagamentos.FindAsync(id);
            if (existente == null)
                throw new Exception("Pagamento não encontrado.");

            _context.Entry(existente).CurrentValues.SetValues(pagamentoAtualizado);
            await _context.SaveChangesAsync();
            return existente;
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

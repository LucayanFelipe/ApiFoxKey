using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class VendaService
    {
        private readonly AppDbContext _context;

        public VendaService(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<ICollection<Venda>> GetAll()
        {
            return await _context.Vendas
                .Include(v => v.Caixa)
                .Include(v => v.ClientePf)
                .Include(v => v.ClientePj)
                .ToListAsync();
        }

        // GET ONE BY ID
        public async Task<Venda> GetOneById(int id)
        {
            var venda = await _context.Vendas
                .Include(v => v.Caixa)
                .Include(v => v.ClientePf)
                .Include(v => v.ClientePj)
                .FirstOrDefaultAsync(v => v.Id_venda == id);

            return venda ?? throw new Exception("Venda não encontrada.");
        }

        // CREATE
        public async Task<Venda> Create(VendaDto venda)
        {
            var novaVenda = new Venda {
                Data_gerada = venda.Data_gerada,
                Hora = venda.Hora,
                Valor_total = venda.Valor_total,
                Desconto = venda.Desconto,
                Valor_final = venda.Valor_final,
                Forma_pagamento = venda.Forma_pagamento,
                Status_venda = venda.Status_venda,
                Id_caixa_fk = venda.Id_caixa_fk,
                Id_cliente_pf_fk = venda.Id_cliente_pf_fk,
                Id_cliente_pj_fk = venda.Id_cliente_pj_fk,
            };

            _context.Vendas.Add(novaVenda);
            await _context.SaveChangesAsync();
            return novaVenda;
        }

        // UPDATE
        public async Task<Venda> Update(int id, VendaDto novaVenda)
        {
            var venda = await _context.Vendas.FindAsync(id);

            if (venda == null)
                throw new Exception("Venda não encontrada.");

            // Atualiza os campos
            venda.Data_gerada = novaVenda.Data_gerada;
            venda.Hora = novaVenda.Hora;
            venda.Valor_total = novaVenda.Valor_total;
            venda.Desconto = novaVenda.Desconto;
            venda.Valor_final = novaVenda.Valor_final;
            venda.Forma_pagamento = novaVenda.Forma_pagamento;
            venda.Status_venda = novaVenda.Status_venda;
            venda.Id_caixa_fk = novaVenda.Id_caixa_fk;
            venda.Id_cliente_pf_fk = novaVenda.Id_cliente_pf_fk;
            venda.Id_cliente_pj_fk = novaVenda.Id_cliente_pj_fk;

            await _context.SaveChangesAsync();
            return venda;
        }

        // DELETE
        public async Task<Venda> Delete(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
                throw new Exception("Venda não encontrada.");

            _context.Vendas.Remove(venda);
            await _context.SaveChangesAsync();
            return venda;
        }
    }
}

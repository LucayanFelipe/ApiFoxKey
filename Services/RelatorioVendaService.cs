using ApiLocadora.DataContexts;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLocadora.Services
{
    public class RelatorioVendaService
    {
        private readonly AppDbContext _context;

        public RelatorioVendaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<RelatorioVenda>> GetAll()
        {
            return await _context.RelatorioVendas.ToListAsync();
        }

        public async Task<RelatorioVenda> GetOneById(int id)
        {
            try
            {
                return await _context.RelatorioVendas.SingleOrDefaultAsync(x => x.Id_relatorio_venda == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RelatorioVenda> Create(RelatorioVenda item)
        {
            try
            {
                await _context.RelatorioVendas.AddAsync(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RelatorioVenda> Update(int id, RelatorioVenda item)
        {
            try
            {
                var relatorio = await _context.RelatorioVendas.FirstOrDefaultAsync(x => x.Id_relatorio_venda == id);

                if (relatorio == null)
                    throw new Exception("Relatório de Venda não encontrado");

                relatorio.Data_cadastro = item.Data_cadastro;
                relatorio.Total_vendas = item.Total_vendas;
                relatorio.Total_recibo = item.Total_recibo;

                await _context.SaveChangesAsync();
                return relatorio;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RelatorioVenda> Delete(int id)
        {
            try
            {
                var relatorio = await _context.RelatorioVendas.FirstOrDefaultAsync(x => x.Id_relatorio_venda == id);
                if (relatorio == null)
                    return null;

                _context.RelatorioVendas.Remove(relatorio);
                await _context.SaveChangesAsync();
                return relatorio;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

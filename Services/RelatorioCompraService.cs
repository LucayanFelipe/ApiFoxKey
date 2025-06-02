using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLocadora.Services
{
    public class RelatorioCompraService
    {
        private readonly AppDbContext _context;

        public RelatorioCompraService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<RelatorioCompra>> GetAll()
        {
            return await _context.RelatorioCompras.ToListAsync();
        }

        public async Task<RelatorioCompra> GetOneById(int id)
        {
            try
            {
                return await _context.RelatorioCompras.SingleOrDefaultAsync(x => x.Id_relatorio_compra == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RelatorioCompra> Create(RelatorioCompraDto item)
        {
            try
            {
                var relatorio = new RelatorioCompra
                {
                    Data_inicial = item.Data_inicial,
                    Data_final = item.Data_final,
                    Total_compras = item.Total_compras,
                    Qtd_compras = item.Qtd_compras,
                    Data_gerada = item.Data_gerada,
                    Produto_frequente = item.Produto_frequente,
                    Fornecedor_frequente = item.Fornecedor_frequente
                };

                await _context.RelatorioCompras.AddAsync(relatorio);
                await _context.SaveChangesAsync();

                return relatorio;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RelatorioCompra> Update(int id, RelatorioCompraDto item)
        {
            try
            {
                var relatorio = await _context.RelatorioCompras.FirstOrDefaultAsync(x => x.Id_relatorio_compra == id);

                if (relatorio == null)
                    throw new Exception("Relatório de compra não encontrado");

                relatorio.Data_inicial = item.Data_inicial;
                relatorio.Data_final = item.Data_final;
                relatorio.Total_compras = item.Total_compras;
                relatorio.Qtd_compras = item.Qtd_compras;
                relatorio.Data_gerada = item.Data_gerada;
                relatorio.Produto_frequente = item.Produto_frequente;
                relatorio.Fornecedor_frequente = item.Fornecedor_frequente;

                await _context.SaveChangesAsync();

                return relatorio;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RelatorioCompra> Delete(int id)
        {
            try
            {
                var relatorio = await _context.RelatorioCompras.FirstOrDefaultAsync(x => x.Id_relatorio_compra == id);
                if (relatorio == null)
                    return null;

                _context.RelatorioCompras.Remove(relatorio);
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

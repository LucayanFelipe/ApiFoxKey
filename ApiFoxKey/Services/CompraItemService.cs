using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLocadora.Services
{
    public class CompraItemService
    {
        private readonly AppDbContext _context;

        public CompraItemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<CompraItem>> GetAll()
        {
            return await _context.CompraItens
                 .Include(e => e.Produto)
                  .Include(e => e.Compra)
                .ToListAsync();
        }

        public async Task<CompraItem> GetOneById(int id)
        {
            try
            {
                return await _context.CompraItens
                                     .Include(e => e.Produto)
                  .Include(e => e.Compra)
                    .SingleOrDefaultAsync(x => x.Id_compra_item == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CompraItem> Create(CompraItemDto item)
        {
            try
            {
                var newCompraItem = new CompraItem
                {
                    Id_compra_fk = item.Id_compra_fk,
                    Id_produto_fk = item.Id_produto_fk,
                    Quantidade = item.Quantidade,
                    Preco_unitario = item.Preco_unitario
                    // Subtotal é calculado pelo banco, não precisa atribuir
                };

                await _context.CompraItens.AddAsync(newCompraItem);
                await _context.SaveChangesAsync();

                return newCompraItem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CompraItem> Update(int id, CompraItemDto item)
        {
            try
            {
                var compraItem = await _context.CompraItens.FirstOrDefaultAsync(x => x.Id_compra_item == id);

                if (compraItem == null)
                    throw new Exception("CompraItem não encontrado");

                compraItem.Id_compra_fk = item.Id_compra_fk;
                compraItem.Id_produto_fk = item.Id_produto_fk;
                compraItem.Quantidade = item.Quantidade;
                compraItem.Preco_unitario = item.Preco_unitario;
                // Subtotal calculado automaticamente pelo banco

                await _context.SaveChangesAsync();

                return compraItem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CompraItem> Delete(int id)
        {
            try
            {
                var compraItem = await _context.CompraItens.FirstOrDefaultAsync(x => x.Id_compra_item == id);
                if (compraItem == null)
                    return null;

                _context.CompraItens.Remove(compraItem);
                await _context.SaveChangesAsync();

                return compraItem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> Exist(int id)
        {
            return await _context.CompraItens.AnyAsync(x => x.Id_compra_item == id);
        }
    }
}

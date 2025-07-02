using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLocadora.Services
{
    public class ItemVendaService
    {
        private readonly AppDbContext _context;

        public ItemVendaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<ItemVenda>> GetAll()
        {
            return await _context.ItensVendas
                                 .Include(i => i.Venda)
                                 .Include(i => i.Produto)
                                 .ToListAsync();
        }

        public async Task<ItemVenda> GetOneById(int id)
        {
            try
            {
                var item = await _context.ItensVendas
                                          .Include(i => i.Venda)
                                          .Include(i => i.Produto)
                                          .SingleOrDefaultAsync(x => x.Id_item_venda == id);

                return item ?? throw new Exception("Item de Venda não encontrado.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ItemVenda> Create(ItemVendaDto dto)
        {
            try
            {
                var novoItem = new ItemVenda
                {
                    Qtd = dto.Qtd,
                    Preco_unit = dto.Preco_unit,
                    Id_venda_fk = dto.Id_venda_fk,
                    Id_produto_fk = dto.Id_produto_fk
                };

                await _context.ItensVendas.AddAsync(novoItem);
                await _context.SaveChangesAsync();

                return novoItem;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }

        public async Task<ItemVenda> Update(int id, ItemVendaDto dto)
        {
            try
            {
                var item = await _context.ItensVendas.FirstOrDefaultAsync(x => x.Id_item_venda == id);

                if (item == null)
                    throw new Exception("Item de Venda não encontrado.");

                item.Qtd = dto.Qtd;
                item.Preco_unit = dto.Preco_unit;
                item.Id_venda_fk = dto.Id_venda_fk;
                item.Id_produto_fk = dto.Id_produto_fk;

                await _context.SaveChangesAsync();

                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ItemVenda> Delete(int id)
        {
            try
            {
                var item = await _context.ItensVendas.FirstOrDefaultAsync(x => x.Id_item_venda == id);
                if (item == null)
                    return null;

                _context.ItensVendas.Remove(item);
                await _context.SaveChangesAsync();

                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLocadora.Services
{
    public class CompraService
    {
        private readonly AppDbContext _context;

        public CompraService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<CompraDetailsDto>> GetAll()
        {
            var compras = await _context.Compras
                .Include(c => c.FornecedorPf)
                .Include(c => c.FornecedorPj)
                .Include(c => c.LoginExclusivo)
                .Include(c => c.ItensCompra) // você precisa configurar isso no model de Compra se ainda não estiver
                    .ThenInclude(ic => ic.Produto)
                .ToListAsync();

            return compras.Select(c => new CompraDetailsDto
            {
                Id_compra = c.Id_compra,
                Data_compra = c.Data_compra,
                Valor_total = c.Valor_total,
                Tipo_pagamento = c.Tipo_pagamento,
                Observacao = c.Observacao,
                Status_compra = c.Status_compra,
                NomeFornecedorPf = c.FornecedorPf?.Nome,
                NomeFornecedorPj = c.FornecedorPj?.Razao_social,
                ItensCompra = c.ItensCompra?.Select(ic => new ItemCompraDetalhadaDto
                {
                    Id_compra_item = ic.Id_compra_item,
                    Quantidade = ic.Quantidade,
                    Preco_unitario = ic.Preco_unitario,
                    NomeProduto = ic.Produto?.Nome
                }).ToList()
            }).ToList();
        }


        public async Task<Compra> GetOneById(int id)
        {
            try
            {
                return await _context.Compras
                            .Include(c => c.FornecedorPf)
        .Include(c => c.FornecedorPj)
                        .Include(c => c.LoginExclusivo)
                    .SingleOrDefaultAsync(x => x.Id_compra == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Compra> Create(CompraDto item)
        {
            try
            {
                var newCompra = new Compra
                {
                    Data_compra = item.Data_compra,
                    Valor_total = item.Valor_total,
                    Tipo_pagamento = item.Tipo_pagamento,
                    Observacao = item.Observacao,
                    Status_compra = item.Status_compra,
                    Id_fornecedor_pf_fk = item.Id_fornecedor_pf_fk,
                    Id_fornecedor_pj_fk = item.Id_fornecedor_pj_fk,
                    Id_login_fk = item.Id_login_fk
                };

                await _context.Compras.AddAsync(newCompra);
                await _context.SaveChangesAsync();

                return newCompra;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Compra> Update(int id, CompraDto item)
        {
            try
            {
                var compra = await _context.Compras.FirstOrDefaultAsync(x => x.Id_compra == id);

                if (compra == null)
                    throw new Exception("Compra não encontrada");

                compra.Data_compra = item.Data_compra;
                compra.Valor_total = item.Valor_total;
                compra.Tipo_pagamento = item.Tipo_pagamento;
                compra.Observacao = item.Observacao;
                compra.Status_compra = item.Status_compra;
                compra.Id_fornecedor_pf_fk = item.Id_fornecedor_pf_fk;
                compra.Id_fornecedor_pj_fk = item.Id_fornecedor_pj_fk;
                compra.Id_login_fk = item.Id_login_fk;

                await _context.SaveChangesAsync();

                return compra;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Compra> Delete(int id)
        {
            try
            {
                var compra = await _context.Compras.FirstOrDefaultAsync(x => x.Id_compra == id);
                if (compra == null)
                    return null;

                _context.Compras.Remove(compra);
                await _context.SaveChangesAsync();

                return compra;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

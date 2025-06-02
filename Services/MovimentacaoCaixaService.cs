using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLocadora.Services
{
    public class MovimentacaoCaixaService
    {
        private readonly AppDbContext _context;

        public MovimentacaoCaixaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<MovimentacaoCaixa>> GetAll()
        {
            return await _context.MovimentacaoCaixas.ToListAsync();
        }

        public async Task<MovimentacaoCaixa> GetOneById(int id)
        {
            try
            {
                return await _context.MovimentacaoCaixas.SingleOrDefaultAsync(x => x.Id_movimentacao_caixa == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MovimentacaoCaixa> Create(MovimentacaoCaixaDto item)
        {
            try
            {
                var newMovimentacao = new MovimentacaoCaixa
                {
                    Tipo = item.Tipo,
                    Valor = item.Valor,
                    Descricao = item.Descricao,
                    Data_gerada = item.Data_gerada.Value,
                    Hora = item.Hora
                };

                await _context.MovimentacaoCaixas.AddAsync(newMovimentacao);
                await _context.SaveChangesAsync();

                return newMovimentacao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MovimentacaoCaixa> Update(int id, MovimentacaoCaixaDto item)
        {
            try
            {
                var movimentacao = await _context.MovimentacaoCaixas.FirstOrDefaultAsync(x => x.Id_movimentacao_caixa == id);

                if (movimentacao == null)
                    throw new Exception("Movimentação de Caixa não encontrada");

                movimentacao.Tipo = item.Tipo;
                movimentacao.Valor = item.Valor;
                movimentacao.Descricao = item.Descricao;
                movimentacao.Data_gerada = item.Data_gerada.Value;
                movimentacao.Hora = item.Hora;

                await _context.SaveChangesAsync();

                return movimentacao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MovimentacaoCaixa> Delete(int id)
        {
            try
            {
                var movimentacao = await _context.MovimentacaoCaixas.FirstOrDefaultAsync(x => x.Id_movimentacao_caixa == id);
                if (movimentacao == null)
                    return null;

                _context.MovimentacaoCaixas.Remove(movimentacao);
                await _context.SaveChangesAsync();

                return movimentacao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> Exist(int id)
        {
            return await _context.MovimentacaoCaixas.AnyAsync(x => x.Id_movimentacao_caixa == id);
        }
    }
}

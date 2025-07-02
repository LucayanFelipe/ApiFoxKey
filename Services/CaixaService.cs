using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLocadora.Services
{
    public class CaixaService
    {
        private readonly AppDbContext _context;

        public CaixaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Caixa>> GetAll()
        {
            return await _context.Caixas
                .Include(c => c.Funcionario)
                .Include(c => c.LoginExclusivo)
                .Include(c => c.MovimentacaoCaixa)
                .ToListAsync();
        }

        public async Task<Caixa> GetOneById(int id)
        {
            try
            {
                return await _context.Caixas
                    .Include(c => c.Funcionario)
                    .Include(c => c.LoginExclusivo)
                    .Include(c => c.MovimentacaoCaixa)
                    .SingleOrDefaultAsync(x => x.Id_caixa == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Caixa> Create(CaixaDto item)
        {

            // Mapeamento manual do DTO para a entidade
            var caixa = new Caixa
            {
                Data_abertura = item.Data_abertura,
                Data_fechamento = item.Data_fechamento,
                Saldo_inicial = item.Saldo_inicial,
                Saldo_final = item.Saldo_final,
                Total_entrada = item.Total_entrada,
                Id_funcionario_fk = item.Id_funcionario_fk,
                Id_login_fk = item.Id_login_fk,
                Id_movimentacao_fk = item.Id_movimentacao_fk
            };

            try
            {
                await _context.Caixas.AddAsync(caixa);
                await _context.SaveChangesAsync();
                return caixa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Caixa> Update(int id, CaixaDto item)
        {
            try
            {
                var caixa = await _context.Caixas.FirstOrDefaultAsync(x => x.Id_caixa == id);
                if (caixa == null)
                    throw new Exception("Caixa não encontrado");

                caixa.Data_abertura = item.Data_abertura;
                caixa.Data_fechamento = item.Data_fechamento;
                caixa.Saldo_inicial = item.Saldo_inicial;
                caixa.Saldo_final = item.Saldo_final;
                caixa.Total_entrada = item.Total_entrada;
                caixa.Id_funcionario_fk = item.Id_funcionario_fk;
                caixa.Id_login_fk = item.Id_login_fk;
                caixa.Id_movimentacao_fk = item.Id_movimentacao_fk;

                await _context.SaveChangesAsync();

                return caixa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Caixa> Delete(int id)
        {
            try
            {
                var caixa = await _context.Caixas.FirstOrDefaultAsync(x => x.Id_caixa == id);
                if (caixa == null)
                    return null;

                _context.Caixas.Remove(caixa);
                await _context.SaveChangesAsync();

                return caixa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> Exist(int id)
        {
            return await _context.Caixas.AnyAsync(x => x.Id_caixa == id);
        }
    }
}

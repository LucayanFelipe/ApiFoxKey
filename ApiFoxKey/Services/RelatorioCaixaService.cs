using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLocadora.Services
{
    public class RelatorioCaixaService
    {
        private readonly AppDbContext _context;

        public RelatorioCaixaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<RelatorioCaixa>> GetAll()
        {
            return await _context.RelatorioCaixas
                                .Include(e => e.Caixa)
                                .ThenInclude(e=> e.MovimentacaoCaixa)
                                .Include(e => e.Caixa)
                                .ThenInclude(e=> e.Funcionario)
                               
                .ToListAsync();
        }

        public async Task<RelatorioCaixa> GetOneById(int id)
        {
            try
            {
                return await _context.RelatorioCaixas
                                    .Include(e => e.Caixa)
                    .SingleOrDefaultAsync(x => x.Id_relatorio_caixa == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RelatorioCaixa> Create(RelatorioCaixaDto item)
        {
            try
            {
                var newRelatorioCaixa = new RelatorioCaixa
                {
                    Data_abertura = item.Data_abertura,
                    Data_fechamento = item.Data_fechamento,
                    Operador = item.Operador,
                    Saldo_inicial = item.Saldo_inicial,
                    Entrada_vendas = item.Entrada_vendas,
                    Entrada_reforco = item.Entrada_reforco,
                    Total_entradas = item.Total_entradas,
                    Saida_sangria = item.Saida_sangria,
                    Saida_despesa = item.Saida_despesa,
                    Total_saida = item.Total_saida,
                    Saldo_final = item.Saldo_final,
                    Observacoes = item.Observacoes,
                    Data_gerada = item.Data_gerada,
                    Id_caixa_fk = item.Id_caixa_fk
                };

                await _context.RelatorioCaixas.AddAsync(newRelatorioCaixa);
                await _context.SaveChangesAsync();

                return newRelatorioCaixa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RelatorioCaixa> Update(int id, RelatorioCaixaDto item)
        {
            try
            {
                var relatorioCaixa = await _context.RelatorioCaixas.FirstOrDefaultAsync(x => x.Id_relatorio_caixa == id);

                if (relatorioCaixa == null)
                    throw new Exception("RelatorioCaixa not found");

                relatorioCaixa.Data_abertura = item.Data_abertura;
                relatorioCaixa.Data_fechamento = item.Data_fechamento;
                relatorioCaixa.Operador = item.Operador;
                relatorioCaixa.Saldo_inicial = item.Saldo_inicial;
                relatorioCaixa.Entrada_vendas = item.Entrada_vendas;
                relatorioCaixa.Entrada_reforco = item.Entrada_reforco;
                relatorioCaixa.Total_entradas = item.Total_entradas;
                relatorioCaixa.Saida_sangria = item.Saida_sangria;
                relatorioCaixa.Saida_despesa = item.Saida_despesa;
                relatorioCaixa.Total_saida = item.Total_saida;
                relatorioCaixa.Saldo_final = item.Saldo_final;
                relatorioCaixa.Observacoes = item.Observacoes;
                relatorioCaixa.Data_gerada = item.Data_gerada;
                relatorioCaixa.Id_caixa_fk = item.Id_caixa_fk;

                await _context.SaveChangesAsync();

                return relatorioCaixa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RelatorioCaixa> Delete(int id)
        {
            try
            {
                var relatorioCaixa = await _context.RelatorioCaixas.FirstOrDefaultAsync(x => x.Id_relatorio_caixa == id);
                if (relatorioCaixa == null)
                    return null;

                _context.RelatorioCaixas.Remove(relatorioCaixa);
                await _context.SaveChangesAsync();

                return relatorioCaixa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

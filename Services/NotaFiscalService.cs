using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLocadora.Services
{
    public class NotaFiscalService
    {
        private readonly AppDbContext _context;

        public NotaFiscalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<NotaFiscal>> GetAll()
        {
            return await _context.NotasFiscais
                .Include(n => n.Venda)
                .ToListAsync();
        }

        public async Task<NotaFiscal> GetOneById(int id)
        {
            try
            {
                var nota = await _context.NotasFiscais
                    .Include(n => n.Venda)
                    .SingleOrDefaultAsync(x => x.Id_nota_fiscal == id);

                return nota ?? throw new Exception("Nota Fiscal não encontrada.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<NotaFiscal> Create(NotaFiscalDto item)
        {
            try
            {
                var novaNota = new NotaFiscal
                {
                    Numero = item.Numero,
                    Data_emissao = item.Data_emissao,
                    Valor_total = item.Valor_total,
                    Tipo = item.Tipo,
                    Chave_acesso = item.Chave_acesso,
                    Xml_nota = item.Xml_nota,
                    Id_venda_fk = item.Id_venda_fk
                };

                await _context.NotasFiscais.AddAsync(novaNota);
                await _context.SaveChangesAsync();

                return novaNota;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<NotaFiscal> Update(int id, NotaFiscalDto item)
        {
            try
            {
                var nota = await _context.NotasFiscais.FirstOrDefaultAsync(x => x.Id_nota_fiscal == id);

                if (nota == null)
                    throw new Exception("Nota Fiscal não encontrada.");

                nota.Numero = item.Numero;
                nota.Data_emissao = item.Data_emissao;
                nota.Valor_total = item.Valor_total;
                nota.Tipo = item.Tipo;
                nota.Chave_acesso = item.Chave_acesso;
                nota.Xml_nota = item.Xml_nota;
                nota.Id_venda_fk = item.Id_venda_fk;

                await _context.SaveChangesAsync();

                return nota;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<NotaFiscal> Delete(int id)
        {
            try
            {
                var nota = await _context.NotasFiscais.FirstOrDefaultAsync(x => x.Id_nota_fiscal == id);
                if (nota == null)
                    return null;

                _context.NotasFiscais.Remove(nota);
                await _context.SaveChangesAsync();

                return nota;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

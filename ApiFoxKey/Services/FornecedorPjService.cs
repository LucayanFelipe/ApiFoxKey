using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class FornecedorPjService
    {
        private readonly AppDbContext _context;

        public FornecedorPjService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<FornecedorPj>> GetAll()
        {
            return await _context.fornecedorpj.ToListAsync();
        }

        public async Task<FornecedorPj> GetOneById(int id)
        {
            var item = await _context.fornecedorpj.FirstOrDefaultAsync(x => x.Id_fornecedor_pj == id);
            if (item == null) throw new Exception("Fornecedor PJ não encontrado");
            return item;
        }

        public async Task<FornecedorPj> Create(FornecedorPjDto dto)
        {
            var novo = new FornecedorPj
            {
                Nome_fantasia = dto.Nome_fantasia,
                Razao_social = dto.Razao_social,
                Inscricao_municipal = dto.Inscricao_municipal,
                Cnpj = dto.Cnpj,
                Data_abertura = dto.Data_abertura,
                Representante = dto.Representante,
                Id_endereco_contato_fk = dto.Id_endereco_contato_fk
            };

            await _context.fornecedorpj.AddAsync(novo);
            await _context.SaveChangesAsync();
            return novo;
        }

        public async Task<FornecedorPj> Update(int id, FornecedorPjDto dto)
        {
            var item = await _context.fornecedorpj.FirstOrDefaultAsync(x => x.Id_fornecedor_pj == id);
            if (item == null) throw new Exception("Fornecedor PJ não encontrado");

            item.Nome_fantasia = dto.Nome_fantasia;
            item.Razao_social = dto.Razao_social;
            item.Inscricao_municipal = dto.Inscricao_municipal;
            item.Cnpj = dto.Cnpj;
            item.Data_abertura = dto.Data_abertura;
            item.Representante = dto.Representante;
            item.Id_endereco_contato_fk = dto.Id_endereco_contato_fk;

            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<FornecedorPj> Delete(int id)
        {
            var item = await _context.fornecedorpj.FirstOrDefaultAsync(x => x.Id_fornecedor_pj == id);
            if (item == null) throw new Exception("Fornecedor PJ não encontrado");

            _context.fornecedorpj.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}

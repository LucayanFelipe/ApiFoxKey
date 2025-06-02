using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class FornecedorPfService
    {
        private readonly AppDbContext _context;

        public FornecedorPfService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<FornecedorPf>> GetAll()
        {
            return await _context.FornecedorPfs
                                .Include(e => e.EnderecoContato)
                .ToListAsync();
        }

        public async Task<FornecedorPf> GetOneById(int id)
        {
            var item = await _context.FornecedorPfs
                .Include(e => e.EnderecoContato)
               .SingleOrDefaultAsync(x => x.Id_fornecedor_pf == id);
            return item ?? throw new Exception("Fornecedor PF não encontrado.");
        }

        public async Task<FornecedorPf> Create(FornecedorPfDto dto)
        {
            var novo = new FornecedorPf
            {
                Nome = dto.Nome,
                Sobrenome = dto.Sobrenome,
                Cpf = dto.Cpf,
                Data_nascimento = dto.Data_nascimento,
                Rg = dto.Rg,
                Sexo = dto.Sexo,
                Estado_civil = dto.Estado_civil,
                Orgao_expedidor = dto.Orgao_expedidor,
                Nacionalidade = dto.Nacionalidade,
                Raca = dto.Raca,
                Id_endereco_contato_fk = dto.Id_endereco_contato_fk
            };

            await _context.FornecedorPfs.AddAsync(novo);
            await _context.SaveChangesAsync();
            return novo;
        }

        public async Task<FornecedorPf> Update(int id, FornecedorPfDto dto)
        {
            var item = await _context.FornecedorPfs.FindAsync(id);
            if (item == null) throw new Exception("Fornecedor PF não encontrado.");

            item.Nome = dto.Nome;
            item.Sobrenome = dto.Sobrenome;
            item.Cpf = dto.Cpf;
            item.Data_nascimento = dto.Data_nascimento;
            item.Rg = dto.Rg;
            item.Sexo = dto.Sexo;
            item.Estado_civil = dto.Estado_civil;
            item.Orgao_expedidor = dto.Orgao_expedidor;
            item.Nacionalidade = dto.Nacionalidade;
            item.Raca = dto.Raca;
            item.Id_endereco_contato_fk = dto.Id_endereco_contato_fk;

            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<FornecedorPf> Delete(int id)
        {
            var item = await _context.FornecedorPfs.FindAsync(id);
            if (item == null) throw new Exception("Fornecedor PF não encontrado.");

            _context.FornecedorPfs.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        private async Task<bool> Exists(int id)
        {
            return await _context.FornecedorPfs.AnyAsync(x => x.Id_fornecedor_pf == id);
        }
    }
}

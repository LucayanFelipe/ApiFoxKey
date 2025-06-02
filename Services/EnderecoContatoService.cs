using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class EnderecoContatoService
    {
        private readonly AppDbContext _context;

        public EnderecoContatoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<EnderecoContato>> GetAll()
        {
            return await _context.EnderecoContatos
                .ToListAsync();
        }

        public async Task<EnderecoContato?> GetOneById(int id)
        {
            return await _context.EnderecoContatos.SingleOrDefaultAsync(x => x.Id_endereco_contato == id);
        }

        public async Task<EnderecoContato> Create(EnderecoContatoDto item)
        {
            var endereco = new EnderecoContato
            {
                Rua = item.Rua,
                Numero = item.Numero,
                Bairro = item.Bairro,
                Complemento = item.Complemento,
                Referencia = item.Referencia,
                Cep = item.Cep,
                Estado = item.Estado,
                Cidade = item.Cidade,
                Email = item.Email,
                Celular = item.Celular
            };

            await _context.EnderecoContatos.AddAsync(endereco);
            await _context.SaveChangesAsync();

            return endereco;
        }

        public async Task<EnderecoContato?> Update(int id, EnderecoContatoDto item)
        {
            var endereco = await _context.EnderecoContatos.FirstOrDefaultAsync(x => x.Id_endereco_contato == id);

            if (endereco == null) return null;

            endereco.Rua = item.Rua;
            endereco.Numero = item.Numero;
            endereco.Bairro = item.Bairro;
            endereco.Complemento = item.Complemento;
            endereco.Referencia = item.Referencia;
            endereco.Cep = item.Cep;
            endereco.Estado = item.Estado;
            endereco.Cidade = item.Cidade;
            endereco.Email = item.Email;
            endereco.Celular = item.Celular;

            await _context.SaveChangesAsync();

            return endereco;
        }

        public async Task<EnderecoContato?> Delete(int id)
        {
            var endereco = await _context.EnderecoContatos.FirstOrDefaultAsync(x => x.Id_endereco_contato == id);

            if (endereco == null) return null;

            _context.EnderecoContatos.Remove(endereco);
            await _context.SaveChangesAsync();

            return endereco;
        }

        private async Task<bool> Exist(int id)
        {
            return await _context.EnderecoContatos.AnyAsync(x => x.Id_endereco_contato == id);
        }
    }
}

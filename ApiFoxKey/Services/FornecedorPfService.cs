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
            try
            {
                var endereco = new EnderecoContato
                {
                    Rua = dto.EnderecoContato.Rua,
                    Numero = dto.EnderecoContato.Numero,
                    Bairro = dto.EnderecoContato.Bairro,
                    Complemento = dto.EnderecoContato.Complemento,
                    Referencia = dto.EnderecoContato.Referencia,
                    Cep = dto.EnderecoContato.Cep,
                    Estado = dto.EnderecoContato.Estado,
                    Cidade = dto.EnderecoContato.Cidade,
                    Email = dto.EnderecoContato.Email,
                    Celular = dto.EnderecoContato.Celular
                };

                await _context.EnderecoContatos.AddAsync(endereco);
                await _context.SaveChangesAsync();
                // Debug
                Console.WriteLine("ID do endereço salvo: " + endereco.Id_endereco_contato);

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
                    Id_endereco_contato_fk = endereco.Id_endereco_contato
                };

                await _context.FornecedorPfs.AddAsync(novo);
                await _context.SaveChangesAsync();
                return novo;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FornecedorPf> Update(int id, FornecedorPfDto dto)
        {
            try
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


                // Busca o endereço associado
                var endereco = await _context.EnderecoContatos
                    .FirstOrDefaultAsync(e => e.Id_endereco_contato == item.Id_endereco_contato_fk);

                if (endereco == null)
                    throw new Exception("Endereço não encontrado");

                // Atualiza os dados do endereço
                endereco.Rua = item.EnderecoContato.Rua;
                endereco.Numero = item.EnderecoContato.Numero;
                endereco.Bairro = item.EnderecoContato.Bairro;
                endereco.Complemento = item.EnderecoContato.Complemento;
                endereco.Referencia = item.EnderecoContato.Referencia;
                endereco.Cep = item.EnderecoContato.Cep;
                endereco.Estado = item.EnderecoContato.Estado;
                endereco.Cidade = item.EnderecoContato.Cidade;
                endereco.Email = item.EnderecoContato.Email;
                endereco.Celular = item.EnderecoContato.Celular;

                await _context.SaveChangesAsync();

                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

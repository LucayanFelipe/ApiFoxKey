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
            return await _context.FornecedorPjs
                 .Include(e => e.EnderecoContato)
                .ToListAsync();
        }

        public async Task<FornecedorPj> GetOneById(int id)
        {
            var item = await _context.FornecedorPjs
                 .Include(e => e.EnderecoContato)
                .FirstOrDefaultAsync(x => x.Id_fornecedor_pj == id);
            if (item == null) throw new Exception("Fornecedor PJ não encontrado");
            return item;
        }

        public async Task<FornecedorPj> Create(FornecedorPjDto dto)
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


                var novo = new FornecedorPj
                {
                    Nome_fantasia = dto.Nome_fantasia,
                    Razao_social = dto.Razao_social,
                    Inscricao_municipal = dto.Inscricao_municipal,
                    Cnpj = dto.Cnpj,
                    Data_abertura = dto.Data_abertura,
                    Representante = dto.Representante,
                    Id_endereco_contato_fk = endereco.Id_endereco_contato
                };

                await _context.FornecedorPjs.AddAsync(novo);
                await _context.SaveChangesAsync();
                return novo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FornecedorPj> Update(int id, FornecedorPjDto dto)
        {
            try
            {
                var item = await _context.FornecedorPjs.FirstOrDefaultAsync(x => x.Id_fornecedor_pj == id);
                if (item == null) throw new Exception("Fornecedor PJ não encontrado");

                item.Nome_fantasia = dto.Nome_fantasia;
                item.Razao_social = dto.Razao_social;
                item.Inscricao_municipal = dto.Inscricao_municipal;
                item.Cnpj = dto.Cnpj;
                item.Data_abertura = dto.Data_abertura;
                item.Representante = dto.Representante;


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

        public async Task<FornecedorPj> Delete(int id)
        {
            var item = await _context.FornecedorPjs.FirstOrDefaultAsync(x => x.Id_fornecedor_pj == id);
            if (item == null) throw new Exception("Fornecedor PJ não encontrado");

            _context.FornecedorPjs.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}

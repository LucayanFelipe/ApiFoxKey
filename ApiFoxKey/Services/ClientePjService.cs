using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class ClientePjService
    {
        private readonly AppDbContext _context;

        public ClientePjService(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<ICollection<ClientePj>> GetAll()
        {
            return await _context.ClientePjs
                .Include(e => e.EnderecoContato)
                .ToListAsync();
        }

        // GET ONE BY ID
        public async Task<ClientePj> GetOneById(int id)
        {
            try
            {
                return await _context.ClientePjs
                    .Include(e => e.EnderecoContato)
                    .SingleOrDefaultAsync(x => x.Id_cliente_pj == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // CREATE
        public async Task<ClientePj> Create(ClientePjDto item)
        {
            try
            {


                var endereco = new EnderecoContato
                {
                    Rua = item.EnderecoContato.Rua,
                    Numero = item.EnderecoContato.Numero,
                    Bairro = item.EnderecoContato.Bairro,
                    Complemento = item.EnderecoContato.Complemento,
                    Referencia = item.EnderecoContato.Referencia,
                    Cep = item.EnderecoContato.Cep,
                    Estado = item.EnderecoContato.Estado,
                    Cidade = item.EnderecoContato.Cidade,
                    Email = item.EnderecoContato.Email,
                    Celular = item.EnderecoContato.Celular
                };

                await _context.EnderecoContatos.AddAsync(endereco);
                await _context.SaveChangesAsync();
                // Debug
                Console.WriteLine("ID do endereço salvo: " + endereco.Id_endereco_contato);

                var novo = new ClientePj
                {
                    Nome_fantasia = item.Nome_fantasia,
                    Razao_social = item.Razao_social,
                    Inscricao_municipal = item.Inscricao_municipal,
                    Cnpj = item.Cnpj,
                    Data_abertura = item.Data_abertura,
                    Representante = item.Representante,
                    Id_endereco_contato_fk = endereco.Id_endereco_contato
                };

                await _context.ClientePjs.AddAsync(novo);
                await _context.SaveChangesAsync();

                return novo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // UPDATE
        public async Task<ClientePj> Update(int id, ClientePjDto item)
        {
            try
            {
                var entidade = await _context.ClientePjs.FirstOrDefaultAsync(x => x.Id_cliente_pj == id);
                if (entidade == null) return null;

                entidade.Nome_fantasia = item.Nome_fantasia;
                entidade.Razao_social = item.Razao_social;
                entidade.Inscricao_municipal = item.Inscricao_municipal;
                entidade.Cnpj = item.Cnpj;
                entidade.Data_abertura = item.Data_abertura;
                entidade.Representante = item.Representante;

                // Busca o endereço associado
                var endereco = await _context.EnderecoContatos
                    .FirstOrDefaultAsync(e => e.Id_endereco_contato == entidade.Id_endereco_contato_fk);

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

                return entidade;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE
        public async Task<ClientePj> Delete(int id)
        {
            try
            {
                var entidade = await _context.ClientePjs.FirstOrDefaultAsync(x => x.Id_cliente_pj == id);
                if (entidade == null) return null;

                _context.ClientePjs.Remove(entidade);
                await _context.SaveChangesAsync();
                return entidade;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // EXIST
        private async Task<bool> Exist(int id)
        {
            return await _context.ClientePjs.AnyAsync(x => x.Id_cliente_pj == id);
        }
    }
}

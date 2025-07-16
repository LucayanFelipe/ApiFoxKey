using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLocadora.Services
{
    public class ClientePfService
    {
        private readonly AppDbContext _context;

        public ClientePfService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<ClientePf>> GetAll()
        {
            return await _context.ClientePfs
                .Include(e => e.EnderecoContato)
                .ToListAsync();
        }

        public async Task<ClientePf> GetOneById(int id)
        {
            try
            {
                return await _context.ClientePfs
                    .Include(e => e.EnderecoContato)
                    .SingleOrDefaultAsync(x => x.Id_cliente_pf == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClientePf> Create(ClientePfDto item)
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


                var newClientePf = new ClientePf
                {
                    Nome = item.Nome,
                    Sobrenome = item.Sobrenome,
                    Data_nascimento = item.Data_nascimento,
                    Cpf = item.Cpf,
                    Rg = item.Rg,
                    Sexo = item.Sexo,
                    Id_endereco_contato_fk = endereco.Id_endereco_contato
                };

                await _context.ClientePfs.AddAsync(newClientePf);
                await _context.SaveChangesAsync();

                return newClientePf;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClientePf> Update(int id, ClientePfDto item)
        {
            try
            {
                var clientePf = await _context.ClientePfs
                    .FirstOrDefaultAsync(x => x.Id_cliente_pf == id);

                if (clientePf == null)
                    throw new Exception("ClientePf not found");

                // Atualiza os dados do cliente
                clientePf.Nome = item.Nome;
                clientePf.Sobrenome = item.Sobrenome;
                clientePf.Data_nascimento = item.Data_nascimento;
                clientePf.Cpf = item.Cpf;
                clientePf.Rg = item.Rg;
                clientePf.Sexo = item.Sexo;

                // Busca o endereço associado
                var endereco = await _context.EnderecoContatos
                    .FirstOrDefaultAsync(e => e.Id_endereco_contato == clientePf.Id_endereco_contato_fk);

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

                return clientePf;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<ClientePf> Delete(int id)
        {
            try
            {
                var clientePf = await _context.ClientePfs
                    .Include(c => c.EnderecoContato)
                    .FirstOrDefaultAsync(x => x.Id_cliente_pf == id);

                if (clientePf == null)   return null;

                // Remove pagamentos relacionados
                var pagamentos = await _context.Pagamentos
                    .Where(p => p.Id_cliente_pf_fk == id)
                    .ToListAsync();
                _context.Pagamentos.RemoveRange(pagamentos);

                // Remove vendas relacionadas
                var vendas = await _context.Vendas
                    .Where(v => v.Id_cliente_pf_fk == id)
                    .ToListAsync();
                _context.Vendas.RemoveRange(vendas);

                // Remove o endereço relacionado, se existir
                if (clientePf.EnderecoContato != null)
                {
                    _context.EnderecoContatos.Remove(clientePf.EnderecoContato);
                }

                // Por fim, remove o cliente
                _context.ClientePfs.Remove(clientePf);

                await _context.SaveChangesAsync();

                return clientePf;
            }
            catch (Exception ex)
            {
                // Retorna erro completo, incluindo inner exception
                throw new Exception("Erro ao excluir cliente: " + ex.Message +
                    (ex.InnerException != null ? " -> " + ex.InnerException.Message : ""));
            }
        }


    }
}

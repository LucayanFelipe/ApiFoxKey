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
            return await _context.clientepf.ToListAsync();
        }

        public async Task<ClientePf> GetOneById(int id)
        {
            try
            {
                return await _context.clientepf.SingleOrDefaultAsync(x => x.Id_cliente_pf == id);
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
                var newClientePf = new ClientePf
                {
                    Nome = item.Nome,
                    Sobrenome = item.Sobrenome,
                    Data_nascimento = item.Data_nascimento,
                    Cpf = item.Cpf,
                    Rg = item.Rg,
                    Sexo = item.Sexo,
                    Id_endereco_contato_fk = item.Id_endereco_contato_fk
                };

                await _context.clientepf.AddAsync(newClientePf);
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
                var clientePf = await _context.clientepf.FirstOrDefaultAsync(x => x.Id_cliente_pf == id);

                if (clientePf == null)
                    throw new Exception("ClientePf not found");

                clientePf.Nome = item.Nome;
                clientePf.Sobrenome = item.Sobrenome;
                clientePf.Data_nascimento = item.Data_nascimento;
                clientePf.Cpf = item.Cpf;
                clientePf.Rg = item.Rg;
                clientePf.Sexo = item.Sexo;
                clientePf.Id_endereco_contato_fk = item.Id_endereco_contato_fk;

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
                var clientePf = await _context.clientepf.FirstOrDefaultAsync(x => x.Id_cliente_pf == id);
                if (clientePf == null)
                    return null;

                _context.clientepf.Remove(clientePf);
                await _context.SaveChangesAsync();

                return clientePf;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> Exist(int id)
        {
            return await _context.clientepf.AnyAsync(x => x.Id_cliente_pf == id);
        }
    }
}

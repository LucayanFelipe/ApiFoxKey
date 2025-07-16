using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class FuncionarioService
    {
        private readonly AppDbContext _context;

        public FuncionarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Funcionario>> GetAll()
        {
            return await _context.Funcionarios
                 .Include(e => e.EnderecoContato)
                .ToListAsync();
        }

        public async Task<Funcionario> GetOneById(int id)
        {
            var funcionario = await _context.Funcionarios
                 .Include(e => e.EnderecoContato)
                .FirstOrDefaultAsync(f => f.Id_funcionario == id);
            if (funcionario == null)
                throw new Exception("Funcionário não encontrado.");

            return funcionario;
        }

        public async Task<Funcionario> Create(FuncionarioDto dto)
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



                var novoFuncionario = new Funcionario
                {
                    Nome = dto.Nome,
                    Sobrenome = dto.Sobrenome,
                    Cpf = dto.Cpf,
                    Rg = dto.Rg,
                    Orgao_expedidor = dto.Orgao_expedidor,
                    Nacionalidade = dto.Nacionalidade,
                    Numero_ctps = dto.Numero_ctps,
                    Numero_pis = dto.Numero_pis,
                    Raca = dto.Raca,
                    Sexo = dto.Sexo,
                    Estado_civil = dto.Estado_civil,
                    Cargo = dto.Cargo,
                    Grau_instrucao = dto.Grau_instrucao,
                    Data_nascimento = dto.Data_nascimento,
                    Id_endereco_contato_fk = endereco.Id_endereco_contato
                };

                await _context.Funcionarios.AddAsync(novoFuncionario);
                await _context.SaveChangesAsync();

                return novoFuncionario;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Funcionario> Update(int id, FuncionarioDto dto)
        {
            try
            {
                var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id_funcionario == id);
                if (funcionario == null)
                    throw new Exception("Funcionário não encontrado.");

                funcionario.Nome = dto.Nome;
                funcionario.Sobrenome = dto.Sobrenome;
                funcionario.Cpf = dto.Cpf;
                funcionario.Rg = dto.Rg;
                funcionario.Orgao_expedidor = dto.Orgao_expedidor;
                funcionario.Nacionalidade = dto.Nacionalidade;
                funcionario.Numero_ctps = dto.Numero_ctps;
                funcionario.Numero_pis = dto.Numero_pis;
                funcionario.Raca = dto.Raca;
                funcionario.Sexo = dto.Sexo;
                funcionario.Estado_civil = dto.Estado_civil;
                funcionario.Cargo = dto.Cargo;
                funcionario.Grau_instrucao = dto.Grau_instrucao;
                funcionario.Data_nascimento = dto.Data_nascimento;

                await _context.SaveChangesAsync();

                // Busca o endereço associado
                var endereco = await _context.EnderecoContatos
                    .FirstOrDefaultAsync(e => e.Id_endereco_contato == funcionario.Id_endereco_contato_fk);

                if (endereco == null)
                    throw new Exception("Endereço não encontrado");

                // Atualiza os dados do endereço
                endereco.Rua = dto.EnderecoContato.Rua;
                endereco.Numero = dto.EnderecoContato.Numero;
                endereco.Bairro = dto.EnderecoContato.Bairro;
                endereco.Complemento = dto.EnderecoContato.Complemento;
                endereco.Referencia = dto.EnderecoContato.Referencia;
                endereco.Cep = dto.EnderecoContato.Cep;
                endereco.Estado = dto.EnderecoContato.Estado;
                endereco.Cidade = dto.EnderecoContato.Cidade;
                endereco.Email = dto.EnderecoContato.Email;
                endereco.Celular = dto.EnderecoContato.Celular;

                await _context.SaveChangesAsync();

                return funcionario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Funcionario> Delete(int id)
        {
            var funcionario = await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id_funcionario == id);
            if (funcionario == null)
                throw new Exception("Funcionário não encontrado.");

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();

            return funcionario;
        }
    }
}

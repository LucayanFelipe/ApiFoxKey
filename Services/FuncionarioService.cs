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
                Id_endereco_contato_fk = dto.Id_endereco_contato_fk
            };

            await _context.Funcionarios.AddAsync(novoFuncionario);
            await _context.SaveChangesAsync();

            return novoFuncionario;
        }

        public async Task<Funcionario> Update(int id, FuncionarioDto dto)
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
            funcionario.Id_endereco_contato_fk = dto.Id_endereco_contato_fk;

            await _context.SaveChangesAsync();

            return funcionario;
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

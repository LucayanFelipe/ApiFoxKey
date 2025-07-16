using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class DespesaService
    {
        private readonly AppDbContext _context;

        public DespesaService(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<ICollection<Despesa>> GetAll()
        {
            return await _context.Despesas
                                .Include(c => c.Login_exclusivo)
                .ToListAsync();
        }

        // GET ONE BY ID
        public async Task<Despesa> GetOneById(int id)
        {
            try
            {
                return await _context.Despesas
                    .Include(d => d.Login_exclusivo)
                    .SingleOrDefaultAsync(d => d.Id_despesa == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // CREATE
        public async Task<Despesa> Create(DespesaDto dto)
        {
            try
            {
                var despesa = new Despesa
                {
                    Tipo_despesa = dto.Tipo_despesa,
                    Valor = dto.Valor,
                    Data_gerada = dto.Data_gerada,
                    Descricao = dto.Descricao,
                    Id_login_fk = dto.Id_login_fk
                };

                await _context.Despesas.AddAsync(despesa);
                await _context.SaveChangesAsync();

                return despesa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // UPDATE
        public async Task<Despesa> Update(int id, DespesaDto dto)
        {
            try
            {
                var despesa = await _context.Despesas.FirstOrDefaultAsync(d => d.Id_despesa == id);
                if (despesa == null) return null;

                despesa.Tipo_despesa = dto.Tipo_despesa;
                despesa.Valor = dto.Valor;
                despesa.Data_gerada = dto.Data_gerada;
                despesa.Descricao = dto.Descricao;
                despesa.Id_login_fk = dto.Id_login_fk;

                await _context.SaveChangesAsync();
                return despesa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE
        public async Task<Despesa> Delete(int id)
        {
            try
            {
                var despesa = await _context.Despesas.FindAsync(id);
                if (despesa == null) return null;

                _context.Despesas.Remove(despesa);
                await _context.SaveChangesAsync();

                return despesa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> Exists(int id)
        {
            return await _context.Despesas.AnyAsync(d => d.Id_despesa == id);
        }
    }
}

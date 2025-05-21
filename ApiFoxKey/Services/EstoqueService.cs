using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class EstoqueService
    {
        private readonly AppDbContext _context;

        public EstoqueService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Estoque>> GetAll()
        {
            return await _context.estoques.ToListAsync();
        }

        public async Task<Estoque> GetOneById(int id)
        {
            try
            {
                return await _context.estoques.SingleOrDefaultAsync(x => x.Id_estoque == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Estoque> Create(EstoqueDto dto)
        {
            try
            {
                var novo = new Estoque
                {
                    Qtd_atual = dto.Qtd_atual,
                    Qtd_reservada = dto.Qtd_reservada,
                    Qtd_minima = dto.Qtd_minima,
                    Status_estoque = dto.Status_estoque,
                    Observacao = dto.Observacao,
                    Id_produto_fk = dto.Id_produto_fk
                };

                await _context.estoques.AddAsync(novo);
                await _context.SaveChangesAsync();

                return novo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Estoque> Update(int id, EstoqueDto dto)
        {
            try
            {
                var estoque = await _context.estoques.FirstOrDefaultAsync(x => x.Id_estoque == id);
                if (estoque == null) return null;

                estoque.Qtd_atual = dto.Qtd_atual;
                estoque.Qtd_reservada = dto.Qtd_reservada;
                estoque.Qtd_minima = dto.Qtd_minima;
                estoque.Status_estoque = dto.Status_estoque;
                estoque.Observacao = dto.Observacao;
                estoque.Id_produto_fk = dto.Id_produto_fk;

                await _context.SaveChangesAsync();

                return estoque;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Estoque> Delete(int id)
        {
            try
            {
                var estoque = await _context.estoques.FirstOrDefaultAsync(x => x.Id_estoque == id);
                if (estoque == null) return null;

                _context.estoques.Remove(estoque);
                await _context.SaveChangesAsync();

                return estoque;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

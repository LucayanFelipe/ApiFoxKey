using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class CategoriaService
    {
        private readonly AppDbContext _context;

        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Categoria>> GetAll()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria?> GetOneById(int id)
        {
            try
            {
                return await _context.Categorias.SingleOrDefaultAsync(x => x.Id_categoria == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Categoria> Create(CategoriaDto item)
        {
            try
            {
                var novaCategoria = new Categoria
                {
                    Nome = item.Nome,
                    Descricao = item.Descricao,
                    Prioridade_reposicao = item.Prioridade_reposicao,
                    Data_registro = item.Data_registro,
                    Ativo = item.Ativo
                };

                await _context.Categorias.AddAsync(novaCategoria);
                await _context.SaveChangesAsync();

                return novaCategoria;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Categoria?> Update(int id, CategoriaDto item)
        {
            try
            {
                var categoria = await _context.Categorias.FirstOrDefaultAsync(x => x.Id_categoria == id);
                if (categoria == null) return null;

                categoria.Nome = item.Nome;
                categoria.Descricao = item.Descricao;
                categoria.Prioridade_reposicao = item.Prioridade_reposicao;
                categoria.Data_registro = item.Data_registro;
                categoria.Ativo = item.Ativo;

                await _context.SaveChangesAsync();
                return categoria;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Categoria?> Delete(int id)
        {
            try
            {
                var categoria = await _context.Categorias.FirstOrDefaultAsync(x => x.Id_categoria == id);
                if (categoria == null) return null;

                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();

                return categoria;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> Exist(int id)
        {
            return await _context.Categorias.AnyAsync(x => x.Id_categoria == id);
        }
    }
}

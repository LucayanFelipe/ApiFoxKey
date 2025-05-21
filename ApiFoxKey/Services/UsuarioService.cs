using ApiLocadora.DataContexts;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Usuario>> GetAll()
        {
            return await _context.usuarios.ToListAsync();
        }

        public async Task<Usuario> GetOneById(int id)
        {
            var usuario = await _context.usuarios.FirstOrDefaultAsync(x => x.Id_usuario == id);
            if (usuario == null) throw new Exception("Usuário não encontrado");
            return usuario;
        }

        public async Task<Usuario> Create(Usuario item)
        {
            try
            {
                await _context.usuarios.AddAsync(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar usuário: {ex.Message}");
            }
        }

        public async Task<Usuario> Update(int id, Usuario item)
        {
            var usuario = await _context.usuarios.FirstOrDefaultAsync(x => x.Id_usuario == id);
            if (usuario == null) throw new Exception("Usuário não encontrado");

            usuario.Nome = item.Nome;
            usuario.Email = item.Email;
            usuario.Senha = item.Senha;
            usuario.PerfilAcesso = item.PerfilAcesso;

            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> Delete(int id)
        {
            var usuario = await _context.usuarios.FirstOrDefaultAsync(x => x.Id_usuario == id);
            if (usuario == null) throw new Exception("Usuário não encontrado");

            _context.usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}

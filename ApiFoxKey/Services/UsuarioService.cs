using ApiLocadora.Models;
using ApiLocadora.Dtos;
using ApiLocadora.DataContexts;
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

        // Retorna todos os usuários
        public async Task<ICollection<Usuario>> GetAll()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // Retorna um usuário pelo ID
        public async Task<Usuario> GetOneById(int id)
        {
            try
            {
                return await _context.Usuarios.SingleOrDefaultAsync(x => x.Id_usuario == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Cria um novo usuário
        public async Task<Usuario> Create(UsuarioDto item)
        {
            try
            {
                var newUsuario = new Usuario
                {
                    Nome = item.Nome,
                    Email = item.Email,
                    Senha = item.Senha,
                    PerfilAcesso = item.Perfil_acesso,
                    
                };
                await _context.Usuarios.AddAsync(newUsuario);
                await _context.SaveChangesAsync();
                return newUsuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Atualiza um usuário existente
        public async Task<Usuario> Update(int id, UsuarioDto dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(f => f.Id_usuario == id);
            if (usuario == null) throw new Exception("Usuário não encontrado.");

            usuario.Nome = dto.Nome;
            usuario.Senha = dto.Senha;
            usuario.Email = dto.Email;
            usuario.PerfilAcesso = dto.Perfil_acesso;
            await _context.SaveChangesAsync();
            return usuario;
        }

        // Remove um usuário
        public async Task<Usuario> Delete(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(f => f.Id_usuario == id);
            if (usuario == null)
                throw new Exception("Funcionário não encontrado.");

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }
    }
}

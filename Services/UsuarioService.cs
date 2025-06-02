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
        public async Task<List<UsuarioDto>> GetAll()
        {
            var usuarios = await _context.Usuarios.ToListAsync();

            return usuarios.Select(u => new UsuarioDto
            {
                Senha = u.Senha,
                Nome = u.Nome,
                Email = u.Email,
                Perfil_acesso = u.PerfilAcesso
            }).ToList();
        }

        // Retorna um usuário pelo ID
        public async Task<UsuarioDto> GetOneById(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id)
                ?? throw new Exception("Usuário não encontrado.");

            return new UsuarioDto
            {
                Nome = usuario.Nome,
                Senha = usuario.Senha,
                Email = usuario.Email,
                Perfil_acesso = usuario.PerfilAcesso
            };
        }

        // Cria um novo usuário
        public async Task<UsuarioDto> Create(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioDto
            {
                Nome = usuario.Nome,
                Senha = usuario.Senha,
                Email = usuario.Email,
                Perfil_acesso = usuario.PerfilAcesso
            };
        }

        // Atualiza um usuário existente
        public async Task<UsuarioDto> Update(int id, Usuario usuarioAtualizado)
        {
            var usuario = await _context.Usuarios.FindAsync(id)
                ?? throw new Exception("Usuário não encontrado.");

            usuario.Nome = usuarioAtualizado.Nome;
            usuario.Email = usuarioAtualizado.Email;
            usuario.Senha = usuarioAtualizado.Senha;
            usuario.PerfilAcesso = usuarioAtualizado.PerfilAcesso;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioDto
            {
                Nome = usuario.Nome,
                Senha = usuario.Senha,
                Email = usuario.Email,
                Perfil_acesso = usuario.PerfilAcesso
            };
        }

        // Remove um usuário
        public async Task<string> Delete(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id)
                ?? throw new Exception("Usuário não encontrado.");

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return $"Usuário com ID {id} deletado com sucesso.";
        }
    }
}

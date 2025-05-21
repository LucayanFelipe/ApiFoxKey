using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class LoginExclusivoService
    {
        private readonly AppDbContext _context;

        public LoginExclusivoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<LoginExclusivo>> GetAll()
        {
            return await _context.loginsexclusivos.ToListAsync();
        }

        public async Task<LoginExclusivo> GetOneById(int id)
        {
            var item = await _context.loginsexclusivos.FirstOrDefaultAsync(x => x.IdLogin == id);
            if (item == null) throw new Exception("LoginExclusivo não encontrado.");
            return item;
        }

        public async Task<LoginExclusivo> Create(LoginExclusivoDto dto)
        {
            var newItem = new LoginExclusivo
            {
                Data_ativacao = dto.Data_ativacao,
                Id_usuario_fk = dto.Id_usuario_fk
            };

            await _context.loginsexclusivos.AddAsync(newItem);
            await _context.SaveChangesAsync();
            return newItem;
        }

        public async Task<LoginExclusivo> Update(int id, LoginExclusivoDto dto)
        {
            var item = await _context.loginsexclusivos.FirstOrDefaultAsync(x => x.IdLogin == id);
            if (item == null) throw new Exception("LoginExclusivo não encontrado.");

            item.Data_ativacao = dto.Data_ativacao;
            item.Id_usuario_fk = dto.Id_usuario_fk;

            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<LoginExclusivo> Delete(int id)
        {
            var item = await _context.loginsexclusivos.FirstOrDefaultAsync(x => x.IdLogin == id);
            if (item == null) throw new Exception("LoginExclusivo não encontrado.");

            _context.loginsexclusivos.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}

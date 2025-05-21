using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class ProdutoService
    {
        private readonly AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Produto>> GetAll()
        {
            return await _context.produtos.ToListAsync();
        }

        public async Task<Produto> GetOneById(int id)
        {
            var item = await _context.produtos.FindAsync(id);
            if (item == null)
                throw new Exception("Produto não encontrado");
            return item;
        }

        public async Task<Produto> Create(ProdutoDto dto)
        {
            var novo = new Produto
            {
                Codigo_barra = dto.Codigo_barra,
                Unidade_medida = dto.Unidade_medida,
                Preco_custo = dto.Preco_custo,
                Preco_venda = dto.Preco_venda,
                Ativo = dto.Ativo,
                Data_cadastro = dto.Data_cadastro,
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Id_categoria_fk = dto.Id_categoria_fk,
                Id_fornecedor_pf_fk = dto.Id_fornecedor_pf_fk,
                Id_fornecedor_pj_fk = dto.Id_fornecedor_pj_fk
            };

            await _context.produtos.AddAsync(novo);
            await _context.SaveChangesAsync();
            return novo;
        }

        public async Task<Produto> Update(int id, ProdutoDto dto)
        {
            var existente = await _context.produtos.FindAsync(id);
            if (existente == null)
                throw new Exception("Produto não encontrado");

            existente.Codigo_barra = dto.Codigo_barra;
            existente.Unidade_medida = dto.Unidade_medida;
            existente.Preco_custo = dto.Preco_custo;
            existente.Preco_venda = dto.Preco_venda;
            existente.Ativo = dto.Ativo;
            existente.Data_cadastro = dto.Data_cadastro;
            existente.Nome = dto.Nome;
            existente.Descricao = dto.Descricao;
            existente.Id_categoria_fk = dto.Id_categoria_fk;
            existente.Id_fornecedor_pf_fk = dto.Id_fornecedor_pf_fk;
            existente.Id_fornecedor_pj_fk = dto.Id_fornecedor_pj_fk;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<Produto> Delete(int id)
        {
            var item = await _context.produtos.FindAsync(id);
            if (item == null)
                throw new Exception("Produto não encontrado");

            _context.produtos.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}

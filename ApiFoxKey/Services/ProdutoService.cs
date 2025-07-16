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
            return await _context.Produtos
                .Include(e => e.Categoria)
                .Include(e => e.FornecedorPf)
                .Include(e => e.FornecedorPj)
                .ToListAsync();
        }

        public async Task<Produto> GetOneById(int id)
        {
            var item = await _context.Produtos
                                .Include(e => e.Categoria)
                .Include(e => e.FornecedorPf)
                .Include(e => e.FornecedorPj)
                .SingleOrDefaultAsync(x => x.Id_produto == id);
            if (item == null)
                throw new Exception("Produto não encontrado");
            return item;
        }

        public async Task<Produto> Create(ProdutoDto dto)
        {
            // Busca categoria existente
            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Nome == dto.Nome_categoria);

            if (categoria == null)
                throw new Exception("Categoria não encontrada");

            // Determina fornecedor PF ou PJ
            FornecedorPf fornecedorPf = null;
            FornecedorPj fornecedorPj = null;

            if (!string.IsNullOrEmpty(dto.Cpf_fornecedor_pf))
            {
                fornecedorPf = await _context.FornecedorPfs.FirstOrDefaultAsync(f => f.Cpf == dto.Cpf_fornecedor_pf);

                if (fornecedorPf == null)
                    throw new Exception("Fornecedor PF não encontrado");
            }
            else if (!string.IsNullOrEmpty(dto.Cnpj_fornecedor_pj))
            {
                fornecedorPj = await _context.FornecedorPjs.FirstOrDefaultAsync(f => f.Cnpj == dto.Cnpj_fornecedor_pj);

                if (fornecedorPj == null)
                    throw new Exception("Fornecedor PJ não encontrado");
            }
            else
            {
                throw new Exception("Nenhum fornecedor informado");
            }

            // Cria produto com os objetos encontrados
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
                Id_categoria_fk = categoria.Id_categoria,
                Id_fornecedor_pf_fk = fornecedorPf?.Id_fornecedor_pf,
                Id_fornecedor_pj_fk = fornecedorPj?.Id_fornecedor_pj
            };

            await _context.Produtos.AddAsync(novo);
            await _context.SaveChangesAsync();

            return novo;
        }

        public async Task<Produto> Update(int id, ProdutoDto dto)
        {
            var existente = await _context.Produtos.FindAsync(id);
            if (existente == null)
                throw new Exception("Produto não encontrado");

            // Buscar a nova categoria
            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Nome == dto.Nome_categoria);
            if (categoria == null)
                throw new Exception("Categoria não encontrada");

            // Determinar fornecedor PF ou PJ
            FornecedorPf fornecedorPf = null;
            FornecedorPj fornecedorPj = null;

            if (!string.IsNullOrEmpty(dto.Cpf_fornecedor_pf))
            {
                fornecedorPf = await _context.FornecedorPfs
                    .FirstOrDefaultAsync(f => f.Cpf == dto.Cpf_fornecedor_pf);
                if (fornecedorPf == null)
                    throw new Exception("Fornecedor PF não encontrado");

                // Zera o outro campo para garantir exclusividade
                existente.Id_fornecedor_pf_fk = fornecedorPf.Id_fornecedor_pf;
                existente.Id_fornecedor_pj_fk = null;
            }
            else if (!string.IsNullOrEmpty(dto.Cnpj_fornecedor_pj))
            {
                fornecedorPj = await _context.FornecedorPjs
                    .FirstOrDefaultAsync(f => f.Cnpj == dto.Cnpj_fornecedor_pj);
                if (fornecedorPj == null)
                    throw new Exception("Fornecedor PJ não encontrado");

                existente.Id_fornecedor_pf_fk = null;
                existente.Id_fornecedor_pj_fk = fornecedorPj.Id_fornecedor_pj;
            }
            else
            {
                throw new Exception("Nenhum fornecedor informado");
            }

            // Atualizar os dados restantes
            existente.Codigo_barra = dto.Codigo_barra;
            existente.Unidade_medida = dto.Unidade_medida;
            existente.Preco_custo = dto.Preco_custo;
            existente.Preco_venda = dto.Preco_venda;
            existente.Ativo = dto.Ativo;
            existente.Data_cadastro = dto.Data_cadastro;
            existente.Nome = dto.Nome;
            existente.Descricao = dto.Descricao;
            existente.Id_categoria_fk = categoria.Id_categoria;

            await _context.SaveChangesAsync();
            return existente;
        }


        public async Task<Produto> Delete(int id)
        {
            var item = await _context.Produtos.FindAsync(id);
            if (item == null)
                throw new Exception("Produto não encontrado");

            _context.Produtos.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}

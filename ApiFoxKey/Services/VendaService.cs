using ApiLocadora.DataContexts;
using ApiLocadora.Dtos;
using ApiLocadora.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLocadora.Services
{
    public class VendaService
    {
        private readonly AppDbContext _context;

        public VendaService(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<ICollection<VendaDetailsDto>> GetAll()
        {
            var vendas = await _context.Vendas
                .Include(v => v.ClientePf)
                .Include(v => v.ClientePj)
                .Include(v => v.ItensVenda)
                    .ThenInclude(iv => iv.Produto)
                .ToListAsync();

            return vendas.Select(v => new VendaDetailsDto
            {
                Id_venda = v.Id_venda,
                Data_gerada = v.Data_gerada,
                Valor_total = v.Valor_total,
                Valor_final = v.Valor_final,
                Forma_pagamento = v.Forma_pagamento,
                Desconto = v.Desconto,
                Status_venda = v.Status_venda,
                NomeClientePf = v.ClientePf?.Nome,
                NomeClientePj = v.ClientePj?.Razao_social,
                ItensVenda = v.ItensVenda.Select(iv => new ItemVendaDetalhadaDto
                {
                    Id_item_venda = iv.Id_item_venda,
                    Qtd = iv.Qtd,
                    Preco_unit = iv.Preco_unit,
                    Subtotal = iv.Subtotal,
                    NomeProduto = iv.Produto?.Nome
                }).ToList()
            }).ToList(); 
        }

        // GET ONE BY ID
        public async Task<Venda> GetOneById(int id)
        {
            var venda = await _context.Vendas
                .Include(v => v.Caixa)
                .Include(v => v.ClientePf)
                .Include(v => v.ClientePj)
                .FirstOrDefaultAsync(v => v.Id_venda == id);

            return venda ?? throw new Exception("Venda não encontrada.");
        }

        // CREATE
        public async Task<VendaDetailsDto> Create(VendaDto dto)
        {
            // Validação do caixa
            var caixa = await _context.Caixas.FindAsync(dto.Id_caixa_fk);
            if (caixa == null)
                throw new Exception("Caixa não encontrado.");

            // Validação cliente PF (se informado)
            ClientePf clientePf = null;
            if (dto.Id_cliente_pf_fk.HasValue)
            {
                clientePf = await _context.ClientePfs.FindAsync(dto.Id_cliente_pf_fk.Value);
                if (clientePf == null)
                    throw new Exception("Cliente PF não encontrado.");
            }

            // Validação cliente PJ (se informado)
            ClientePj clientePj = null;
            if (dto.Id_cliente_pj_fk.HasValue)
            {
                clientePj = await _context.ClientePjs.FindAsync(dto.Id_cliente_pj_fk.Value);
                if (clientePj == null)
                    throw new Exception("Cliente PJ não encontrado.");
            }

            // Criação da venda
            var novaVenda = new Venda
            {
                Data_gerada = dto.Data_gerada,
                Hora = dto.Hora,
                Valor_total = dto.Valor_total,
                Desconto = dto.Desconto,
                Valor_final = dto.Valor_final,
                Forma_pagamento = dto.Forma_pagamento,
                Status_venda = dto.Status_venda,
                Id_caixa_fk = dto.Id_caixa_fk,
                Id_cliente_pf_fk = dto.Id_cliente_pf_fk,
                Id_cliente_pj_fk = dto.Id_cliente_pj_fk
            };

            _context.Vendas.Add(novaVenda);
            await _context.SaveChangesAsync(); // Gera o Id_venda

            // Criação dos itens da venda
            var itensVenda = dto.Itens.Select(iv => new ItemVenda
            {
                Qtd = iv.Qtd,
                Preco_unit = iv.Preco_unit,
                Id_venda_fk = novaVenda.Id_venda,
                Id_produto_fk = iv.Id_produto_fk
                // Subtotal será calculado pelo banco
            }).ToList();

            _context.ItensVendas.AddRange(itensVenda);
            await _context.SaveChangesAsync();

            // Buscar os itens com os dados do produto para montar o DTO
            var itensDetalhados = await _context.ItensVendas
                .Where(iv => iv.Id_venda_fk == novaVenda.Id_venda)
                .Include(iv => iv.Produto)
                .Select(iv => new ItemVendaDetalhadaDto
                {
                    Id_item_venda = iv.Id_item_venda,
                    Qtd = iv.Qtd,
                    Preco_unit = iv.Preco_unit,
                    Subtotal = iv.Subtotal,
                    NomeProduto = iv.Produto.Nome
                })
                .ToListAsync();

            // Montar DTO final de resposta
            return new VendaDetailsDto
            {
                Id_venda = novaVenda.Id_venda,
                Data_gerada = novaVenda.Data_gerada,
                Valor_total = novaVenda.Valor_total,
                Desconto = novaVenda.Desconto,
                Valor_final = novaVenda.Valor_final,
                Forma_pagamento = novaVenda.Forma_pagamento,
                Status_venda = novaVenda.Status_venda,
                NomeClientePf = clientePf?.Nome,
                NomeClientePj = clientePj?.Razao_social,
                ItensVenda = itensDetalhados
            };
        }



        // UPDATE
        public async Task<VendaDetailsDto> Update(int id, VendaDto dto)
        {
            var venda = await _context.Vendas
                .Include(v => v.ItensVenda)
                .FirstOrDefaultAsync(v => v.Id_venda == id);

            if (venda == null)
                throw new Exception("Venda não encontrada.");

            // Validações (se desejar, repita as de cliente e caixa como no Create)

            // Atualiza os dados da venda
            venda.Data_gerada = dto.Data_gerada;
            venda.Hora = dto.Hora;
            venda.Valor_total = dto.Valor_total;
            venda.Desconto = dto.Desconto;
            venda.Valor_final = dto.Valor_final;
            venda.Forma_pagamento = dto.Forma_pagamento;
            venda.Status_venda = dto.Status_venda;
            venda.Id_caixa_fk = dto.Id_caixa_fk;
            venda.Id_cliente_pf_fk = dto.Id_cliente_pf_fk;
            venda.Id_cliente_pj_fk = dto.Id_cliente_pj_fk;

            // Remove itens antigos
            _context.ItensVendas.RemoveRange(venda.ItensVenda);

            // Adiciona os novos itens
            var novosItens = dto.Itens.Select(iv => new ItemVenda
            {
                Qtd = iv.Qtd,
                Preco_unit = iv.Preco_unit,
                Id_venda_fk = venda.Id_venda,
                Id_produto_fk = iv.Id_produto_fk
            }).ToList();

            _context.ItensVendas.AddRange(novosItens);

            await _context.SaveChangesAsync();

            // Busca novamente os itens detalhados para retorno
            var itensDetalhados = await _context.ItensVendas
                .Where(iv => iv.Id_venda_fk == venda.Id_venda)
                .Include(iv => iv.Produto)
                .Select(iv => new ItemVendaDetalhadaDto
                {
                    Id_item_venda = iv.Id_item_venda,
                    Qtd = iv.Qtd,
                    Preco_unit = iv.Preco_unit,
                    Subtotal = iv.Subtotal,
                    NomeProduto = iv.Produto.Nome
                })
                .ToListAsync();

            var clientePf = dto.Id_cliente_pf_fk.HasValue
                ? await _context.ClientePfs.FindAsync(dto.Id_cliente_pf_fk.Value)
                : null;

            var clientePj = dto.Id_cliente_pj_fk.HasValue
                ? await _context.ClientePjs.FindAsync(dto.Id_cliente_pj_fk.Value)
                : null;

            return new VendaDetailsDto
            {
                Id_venda = venda.Id_venda,
                Data_gerada = venda.Data_gerada,
                Valor_total = venda.Valor_total,
                Desconto = venda.Desconto,
                Valor_final = venda.Valor_final,
                Forma_pagamento = venda.Forma_pagamento,
                Status_venda = venda.Status_venda,
                NomeClientePf = clientePf?.Nome,
                NomeClientePj = clientePj?.Razao_social,
                ItensVenda = itensDetalhados
            };
        }


        // DELETE
        public async Task<Venda> Delete(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
                throw new Exception("Venda não encontrada.");

            _context.Vendas.Remove(venda);
            await _context.SaveChangesAsync();
            return venda;
        }
    }
}

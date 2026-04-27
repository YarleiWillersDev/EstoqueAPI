using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.DTOs;
using EstoqueApi.Exceptions;
using EstoqueApi.Mappers;
using EstoqueApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApi.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProdutoResponse> CreateAsync(ProdutoRequest request)
        {
            if (request is null)
                throw new ValidationException("O produto não pode ser null");

            var produto = ProdutoMapper.ToEntity(request);

            ValidarProduto(produto);
            await ValidarCategoriaExisteAsync(produto.CategoriaId);

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return ProdutoMapper.ToResponse(produto);
        }

        private void ValidarProduto(Produto produto)
        {
            if (string.IsNullOrWhiteSpace(produto.Nome))
                throw new ValidationException("O nome do produto não pode ser vazio.");
            
            if (produto.Quantidade < 0)
                throw new ValidationException("A quantidade do produto não pode ser menor que 0.");

            if (produto.CategoriaId <= 0)
                throw new ValidationException("O ID da categoria não pode ser menor ou igual a 0");
        }

        private async Task ValidarCategoriaExisteAsync(long categoriaId)
        {
            var categoriaExiste = await _context.Categorias.
                AnyAsync(c => c.Id == categoriaId);

            if (!categoriaExiste)
                throw new NotFoundException("Não existem categorias cadastradas para o ID informado.");
        }

        public async Task DeleteAsync(long id)
        {
            if (id <= 0)
                throw new ValidationException("Id inválido");

            var produto = await _context.Produtos.FindAsync(id);

            if (produto is null)
                throw new NotFoundException("Nenhum produto foi encontrado para o ID informado");

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProdutoSimplesResponse>> GetAllAsync()
        {
            var produtos = await _context.Produtos.ToListAsync();

            return produtos.Select(ProdutoMapper.ToSimplesResponse).ToList();
        }

        public async Task<List<ProdutoSimplesResponse>> GetByCategoriaIdAsync(long categoriaId)
        {
            var produtos = await _context.Produtos
                .Where(p => p.CategoriaId == categoriaId)
                .ToListAsync();

            return produtos.Select(ProdutoMapper.ToSimplesResponse).ToList();
        }

        public async Task<ProdutoResponse> GetByIdAsync(long id)
        {
            if (id <= 0)
                throw new ValidationException("O ID não pode ser menor ou igual a 0.");

            var produto = await _context.Produtos
                .Include(p => p.Movimentacoes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto is null)
                throw new NotFoundException("Nenhum produto encontrado para o ID informado.");

            return ProdutoMapper.ToResponse(produto);
        }
    }
}
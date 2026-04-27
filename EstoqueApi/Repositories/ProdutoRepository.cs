using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.Model;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApi.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Produto produto)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Produto>> GetAllAsync()
            => await _context.Produtos.ToListAsync();

        public async Task<List<Produto>> GetByCategoriaIdAsync(long categoriaId)
            => await _context.Produtos
                    .Where(p => p.CategoriaId == categoriaId)
                    .ToListAsync();

        public async Task<Produto?> GetByIdAsync(long id)
            => await _context.Produtos
                    .Include(m => m.Movimentacoes)
                    .FirstOrDefaultAsync(p => p.Id == id);

        public async Task UpdateAsync(Produto produto)
        {
            await _context.SaveChangesAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.Model;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApi.Repositories
{
    public class MovimentacaoEstoqueRepository : IMovimentacaoEstoqueRepository
    {
        private readonly AppDbContext _context;

        public MovimentacaoEstoqueRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MovimentacaoEstoque movimentacaoEstoque)
        {
            await _context.MovimentacoesEstoque.AddAsync(movimentacaoEstoque);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MovimentacaoEstoque>> GetAllAsync()
            => await _context.MovimentacoesEstoque.ToListAsync();

        public async Task<MovimentacaoEstoque?> GetByIdAsync(long id)
            => await _context.MovimentacoesEstoque
                    .Include(m => m.Produto)
                    .FirstOrDefaultAsync(m => m.Id == id);
    }
}

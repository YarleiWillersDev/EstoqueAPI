using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.Model;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApi.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }
            
        public async Task DeleteAsync(Categoria categoria){
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Categoria>> GetAllAsync()
            => await _context.Categorias.ToListAsync();

        public async Task<Categoria?> GetByIdAsync(long id)
            => await _context.Categorias
                    .Include(p => p.Produtos)
                    .FirstOrDefaultAsync(c => c.Id == id);
    }
}
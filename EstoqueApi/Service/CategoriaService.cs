using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.Model;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApi.Service
{
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _context;

        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Categoria> CreateAsync(Categoria categoria)
        {
            if (categoria is null)
                throw new ArgumentNullException("A categoria não pode ser nula");

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return categoria;
        }

        public async Task DeleteAsync(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Id inválido");

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);

            if (categoria is null)
                throw new KeyNotFoundException("Categoria não encontrada");

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Categoria>> GetAllAsync()
        {
            var categorias = await _context.Categorias.ToListAsync();

            return categorias;
        }

        public async Task<Categoria> GetByIdAsync(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Id inválido");

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);

            if (categoria is null)
                throw new KeyNotFoundException("Categoria não encontrada");

            return categoria;
        }
    }
}
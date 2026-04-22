using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.DTOs;
using EstoqueApi.Exceptions;
using EstoqueApi.Mappers;
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

        public async Task<CategoriaResponse> CreateAsync(CategoriaRequest request)
        {
            if (request is null)
                throw new ValidationException("A categoria não pode ser nula");
            
            ValidarNomeNull(request.Nome);
            
            var categoriaEntity = CategoriaMapper.ToEntity(request);

            _context.Categorias.Add(categoriaEntity);
            await _context.SaveChangesAsync();

            return CategoriaMapper.ToResponse(categoriaEntity);
        }

        private void ValidarNomeNull (string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ValidationException("O nome da categoria não pode ser null.");
        }

        public async Task DeleteAsync(long id)
        {
            if (id <= 0)
                throw new ValidationException("Id inválido");

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);

            if (categoria is null)
                throw new NotFoundException("Nenhuma categoria foi encontrada para o Id informado.");

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CategoriaSimplesResponse>> GetAllAsync()
        {
            var categorias = await _context.Categorias.ToListAsync();

            return categorias.Select(CategoriaMapper.ToSimplesResponse).ToList();
        }

        public async Task<CategoriaResponse> GetByIdAsync(long id)
        {
            if (id <= 0)
                throw new ValidationException("Id inválido");

            var categoria = await _context.Categorias
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (categoria is null)
                throw new NotFoundException("Categoria não encontrada");

            return CategoriaMapper.ToResponse(categoria);
        }
    }
}
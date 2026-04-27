using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.DTOs;
using EstoqueApi.Exceptions;
using EstoqueApi.Mappers;
using EstoqueApi.Model;
using EstoqueApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApi.Service
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<CategoriaResponse> CreateAsync(CategoriaRequest request)
        {
            if (request is null)
                throw new ValidationException("A categoria não pode ser nula");
            
            ValidarNome(request.Nome);
            
            var categoriaEntity = CategoriaMapper.ToEntity(request);

            await _repository.AddAsync(categoriaEntity);
            
            return CategoriaMapper.ToResponse(categoriaEntity);
        }

        private void ValidarNome (string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ValidationException("O nome da categoria não pode ser null.");
        }

        public async Task DeleteAsync(long id)
        {
            if (id <= 0)
                throw new ValidationException("Id inválido");

            var categoria = await _repository.GetByIdAsync(id);

            if (categoria is null)
                throw new NotFoundException("Nenhuma categoria foi encontrada para o Id informado.");

            await _repository.DeleteAsync(categoria);
        }

        public async Task<List<CategoriaSimplesResponse>> GetAllAsync()
        {
            var categorias = await _repository.GetAllAsync();

            return categorias.Select(CategoriaMapper.ToSimplesResponse).ToList();
        }

        public async Task<CategoriaResponse> GetByIdAsync(long id)
        {
            if (id <= 0)
                throw new ValidationException("Id inválido");

            var categoria = await _repository.GetByIdAsync(id);

            if (categoria is null)
                throw new NotFoundException("Categoria não encontrada");

            return CategoriaMapper.ToResponse(categoria);
        }
    }
}
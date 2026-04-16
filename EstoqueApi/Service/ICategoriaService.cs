using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.DTOs;
using EstoqueApi.Model;

namespace EstoqueApi.Service
{
    public interface ICategoriaService
    {
        Task<CategoriaResponse> GetByIdAsync(long id);
        Task<List<CategoriaSimplesResponse>> GetAllAsync();
        Task<CategoriaResponse> CreateAsync(CategoriaRequest categoria);
        Task DeleteAsync(long id);
    }
}
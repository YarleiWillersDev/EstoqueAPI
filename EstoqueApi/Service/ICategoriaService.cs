using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Model;

namespace EstoqueApi.Service
{
    public interface ICategoriaService
    {
        Task<Categoria> GetByIdAsync(long id);
        Task<List<Categoria>> GetAllAsync();
        Task<Categoria> CreateAsync(Categoria categoria);
        Task DeleteAsync(long id);
    }
}
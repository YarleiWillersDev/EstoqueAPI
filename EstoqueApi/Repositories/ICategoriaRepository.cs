using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Model;

namespace EstoqueApi.Repositories
{
    public interface ICategoriaRepository
    {
        public Task AddAsync(Categoria categoria);
        public Task<Categoria?> GetByIdAsync(long id);
        public Task<List<Categoria>> GetAllAsync();
        public Task DeleteAsync(Categoria categoria);
    }
}
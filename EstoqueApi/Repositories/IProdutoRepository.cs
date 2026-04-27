using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Model;

namespace EstoqueApi.Repositories
{
    public interface IProdutoRepository
    {
        public Task<Produto?> GetByIdAsync(long id);
        public Task<List<Produto>> GetByCategoriaIdAsync(long categoriaId);
        public Task<List<Produto>> GetAllAsync();
        public Task AddAsync(Produto produto);
        public Task DeleteAsync(Produto produto);
        public Task UpdateAsync(Produto produto);
    }
}
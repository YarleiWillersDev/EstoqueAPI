using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Model;

namespace EstoqueApi.Service
{
    public interface IProdutoService
    {
        Task<Produto> GetByIdAsync(long id);
        Task<List<Produto>> GetByCategoriaIdAsync(long categoriaID);
        Task<List<Produto>> GetAll();
        Task<Produto> CreateAsync(Produto produto);
        Task Delete(long id);
    }
}
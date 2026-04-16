using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.DTOs;
using EstoqueApi.Model;

namespace EstoqueApi.Service
{
    public interface IProdutoService
    {
        Task<ProdutoResponse> GetByIdAsync(long id);
        Task<List<ProdutoSimplesResponse>> GetByCategoriaIdAsync(long categoriaID);
        Task<List<ProdutoSimplesResponse>> GetAllAsync();
        Task<ProdutoResponse> CreateAsync(ProdutoRequest produto);
        Task DeleteAsync(long id);
    }
}
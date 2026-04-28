using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Model;

namespace EstoqueApi.Repositories
{
    public interface IMovimentacaoEstoqueRepository
    {
        public Task AddAsync(MovimentacaoEstoque movimentacaoEstoque);
        public Task<MovimentacaoEstoque?> GetByIdAsync(long id);
        public Task<List<MovimentacaoEstoque>> GetAllAsync();
    }
}
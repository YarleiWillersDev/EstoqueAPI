using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueApi.Service
{
    public interface IMovimentacaoEstoqueService
    {
        Task<MovimentacaoEstoque> Create(MovimentacaoEstoque movimentacaoEstoque);
        Task<MovimentacaoEstoque> GetById(long id);
        Task<List<MovimentacaoEstoque>> GetAll();

    }
}
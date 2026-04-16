using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.DTOs;
using EstoqueApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueApi.Service
{
    public interface IMovimentacaoEstoqueService
    {
        Task<MovimentacaoEstoqueResponse> Create(MovimentacaoEstoqueRequest request);
        Task<MovimentacaoEstoqueResponse> GetById(long id);
        Task<List<MovimentacaoEstoqueResponse>> GetAll();

    }
}
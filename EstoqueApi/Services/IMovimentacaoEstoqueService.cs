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
        Task<MovimentacaoEstoqueResponse> CreateAsync(MovimentacaoEstoqueRequest request);
        Task<MovimentacaoEstoqueResponse> GetByIdAsync(long id);
        Task<List<MovimentacaoEstoqueResponse>> GetAllAsync();

    }
}
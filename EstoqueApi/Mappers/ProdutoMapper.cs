using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.DTOs;
using EstoqueApi.Model;

namespace EstoqueApi.Mappers
{
    public static class ProdutoMapper
    {
        

        public static ProdutoResponse ToResponse(Produto produto)
        {
            var produtoResponse = new ProdutoResponse(
                produto.Id,
                produto.Nome,
                produto.Quantidade,
                produto.CategoriaId
            );

            return produtoResponse;
        }
    }
}
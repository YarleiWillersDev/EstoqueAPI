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
        
        public static Produto ToEntity(ProdutoRequest request)
        {
            return new Produto(
                request.Nome,
                request.Quantidade,
                request.CategoriaId
            );
        }

        public static Produto ToEntity(ProdutoAtualizarRequest request)
        {
            return new Produto(
                request.Nome,
                0,
                request.CategoriaId
            );
        }

        public static ProdutoResponse ToResponse(Produto produto)
        {
            var movimentacoes = produto.Movimentacoes
                .Select(m => MovimentacaoEstoqueMapper.ToResponse(m))
                .ToList(); 

            return new ProdutoResponse(
                produto.Id,
                produto.Nome,
                produto.Quantidade,
                produto.CategoriaId,
                movimentacoes
            );
        }

        public static ProdutoSimplesResponse ToSimplesResponse(Produto produto)
        {
            return new ProdutoSimplesResponse(produto.Id, produto.Nome);
        }
    }
}
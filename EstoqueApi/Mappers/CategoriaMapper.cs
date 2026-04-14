using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.DTOs;
using EstoqueApi.Model;

namespace EstoqueApi.Mappers
{
    public static class CategoriaMapper
    {
        
        public static Categoria ToEntity(CategoriaRequest request)
        {
            return new Categoria(request.Nome);
        }
        
        public static CategoriaResponse ToResponse(Categoria categoria)
        {
            var produtos = categoria.Produtos
                .Select(p => ProdutoMapper.ToResponse(p))
                .ToList();

            return new CategoriaResponse(
                categoria.Id,
                categoria.Nome,
                produtos
            );
        }
    }
}
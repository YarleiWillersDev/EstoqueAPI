using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.DTOs;
using EstoqueApi.Model;

namespace EstoqueApi.Mappers
{
    public static class MovimentacaoEstoqueMapper
    {
        public static MovimentacaoEstoque ToEntity(MovimentacaoEstoqueRequest request)
        {
            return new MovimentacaoEstoque(
                request.Quantidade,
                request.TipoMovimentacao,
                request.ProdutoId
            );
        }

        public static MovimentcaoEstoqueResponse ToResponse(MovimentacaoEstoque mov)
        {
            return new MovimentcaoEstoqueResponse(
                mov.Id,
                mov.Quantidade,
                mov.TipoMovimentacao,
                mov.DataMovimentacao,
                mov.ProdutoId
            );
        }
    }
}
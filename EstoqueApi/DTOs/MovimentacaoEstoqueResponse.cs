using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Model;

namespace EstoqueApi.DTOs
{
    public class MovimentacaoEstoqueResponse
    {

        public long Id { get; }
        public int Quantidade { get; }
        public TipoMovimentacao TipoMovimentacao { get; }
        public DateTime DataMovimentacao { get; }
        public long ProdutoId { get; }

        public MovimentacaoEstoqueResponse(long id, int quantidade, TipoMovimentacao tipoMovimentacao, DateTime dataMovimentacao, long produtoId)
        {
            Id = id;
            Quantidade = quantidade;
            TipoMovimentacao = tipoMovimentacao;
            DataMovimentacao = dataMovimentacao;
            ProdutoId = produtoId;
        }

    }
}
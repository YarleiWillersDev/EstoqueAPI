using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueApi.Model
{
    public class MovimentacaoEstoque
    {
        public long Id { get; private set; }
        public int Quantidade { get; private set; }
        public TipoMovimentacao TipoMovimentacao { get; private set; }
        public DateTime DataMovimentacao { get; }
        public long ProdutoId { get; private set; }
        public Produto? Produto { get; private set; }

        public MovimentacaoEstoque(int quantidade, TipoMovimentacao tipoMovimentacao, long produtoId)
        {
            Quantidade = quantidade;
            TipoMovimentacao = tipoMovimentacao;
            DataMovimentacao = DateTime.UtcNow;
            ProdutoId = produtoId;
        }
    }
}
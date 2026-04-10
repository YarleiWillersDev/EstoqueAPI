using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueApi.Model
{
    public class Produto
    {
        public long Id { get; private set; }
        public string Nome { get; private set; }
        public long CategoriaId { get; private set; }
        public Categoria? Categoria { get; private set; }
        public ICollection<MovimentacaoEstoque> Movimentacoes { get; set; } = default!;

        public Produto(string nome, long categoriaId)
        {
            Nome = nome;
            CategoriaId = categoriaId;
        }

    }
}
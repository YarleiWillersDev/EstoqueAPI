using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueApi.Model
{
    public class Categoria
    {
        public long Id { get; private set; }
        public string Nome { get; private set; }
        public ICollection<Produto>? Produtos {get; private set; }

        private Categoria()
        {
            Nome = default!;
        }

        public Categoria(string nome)
        {
            Nome = nome;
        }

    }
}
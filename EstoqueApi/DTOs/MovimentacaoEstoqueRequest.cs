using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Model;

namespace EstoqueApi.DTOs
{
    public class MovimentacaoEstoqueRequest
    {

        [Range(1, int.MaxValue)]
        public int Quantidade { get; set; }

        [Required]
        public TipoMovimentacao TipoMovimentacao { get; set; }

        public long ProdutoId { get; set; }

    }
}
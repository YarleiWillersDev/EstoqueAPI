using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueApi.DTOs
{
    public class ProdutoAtualizarRequest
    {
        
        [Required]
        [StringLength(130)]
        public string Nome { get; set; } = null!;
        
        public long CategoriaId { get; set; }

    }
}
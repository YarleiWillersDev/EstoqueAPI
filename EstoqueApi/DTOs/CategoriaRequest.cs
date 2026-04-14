using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Model;

namespace EstoqueApi.DTOs
{
    public class CategoriaRequest
    {

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = null!;

    }
}
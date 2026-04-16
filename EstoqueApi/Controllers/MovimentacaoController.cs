using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Context;
using EstoqueApi.DTOs;
using EstoqueApi.Model;
using EstoqueApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MovimentacaoController : ControllerBase
    {

        private readonly IMovimentacaoEstoqueService _service;

        public MovimentacaoController(IMovimentacaoEstoqueService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovimentacaoEstoqueResponse>> GetById(long id)
        {
            if (id <= 0)
                throw new ArgumentException("O Id da movimentação de estoque não pode ser menor ou igual a 0.");

            var movimentacao = await _service.GetById(id);

            return Ok(movimentacao);
        }

        [HttpGet]
        public async Task<ActionResult<List<MovimentacaoEstoqueResponse>>> GetAll()
        {
            var movimentacoes = await _service.GetAll();

            return Ok(movimentacoes);
        }

        [HttpPost]
        public async Task<ActionResult<MovimentacaoEstoqueResponse>> Create([FromBody] MovimentacaoEstoqueRequest request)
        {
            if (request is null)
                throw new ArgumentNullException("Movimentação de estoque não pode ser null.");

            var novaMovimentcao = await _service.Create(request);

            return CreatedAtAction(nameof(GetById), new { id = novaMovimentcao.Id }, novaMovimentcao);
        }
        
    }
}
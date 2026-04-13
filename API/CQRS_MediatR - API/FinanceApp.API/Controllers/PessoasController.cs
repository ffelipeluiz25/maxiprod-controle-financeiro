using FinanceApp.Application.Features.Pessoas.Commands;
using FinanceApp.Application.Features.Pessoas.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace FinanceApp.API.Controllers
{
    [ApiController]
    [Route("api/pessoas")]
    public class PessoasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PessoasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarPessoaCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var result = await _mediator.Send(new ListarPessoasQuery());
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, EditarPessoaCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id inconsistente");

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var result = await _mediator.Send(new DeletarPessoaCommand(id));
            return Ok(result);
        }

    }
}
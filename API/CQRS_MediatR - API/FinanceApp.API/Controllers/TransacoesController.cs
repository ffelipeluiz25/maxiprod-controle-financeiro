using FinanceApp.Application.Features.Transacoes.Commands;
using FinanceApp.Application.Features.Transacoes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace FinanceApp.API.Controllers
{

    [ApiController]
    [Route("api/transacoes")]
    public class TransacoesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransacoesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarTransacaoCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var result = await _mediator.Send(new ListarTransacoesQuery());
            return Ok(result);
        }

    }
}
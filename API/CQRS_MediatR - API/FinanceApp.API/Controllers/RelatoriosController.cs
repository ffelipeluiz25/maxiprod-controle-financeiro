using FinanceApp.Application.Features.Relatorios.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace FinanceApp.API.Controllers
{
    [ApiController]
    [Route("api/relatorios")]
    public class RelatoriosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RelatoriosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("totais-por-pessoa")]
        public async Task<IActionResult> TotaisPorPessoa()
        {
            var result = await _mediator.Send(new TotaisPorPessoaQuery());
            return Ok(result);
        }

        [HttpGet("totais-por-categoria")]
        public async Task<IActionResult> TotaisPorCategoria()
        {
            var result = await _mediator.Send(new TotaisPorCategoriaQuery());
            return Ok(result);
        }

    }
}
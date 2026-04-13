using FinanceApp.Application.Features.Categorias.Commands;
using FinanceApp.Application.Features.Categorias.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace FinanceApp.API.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarCategoriaCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var result = await _mediator.Send(new ListarCategoriasQuery());
            return Ok(result);
        }

    }
}
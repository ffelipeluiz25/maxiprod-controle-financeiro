using FinanceApp.DTOs.Transacao.Request;
using FinanceApp.Services.Interface;
using Microsoft.AspNetCore.Mvc;
namespace FinanceApp.Controllers
{
    [ApiController]
    [Route("api/transacoes")]
    public class TransacoesController : ControllerBase
    {

        private readonly ITransacaoService _transacaoService;

        public TransacoesController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarTransacaoDTO request)
        {
            var id = await _transacaoService.Criar(request);
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var result = await _transacaoService.ListarTransacoes();
            return Ok(result);
        }

    }
}
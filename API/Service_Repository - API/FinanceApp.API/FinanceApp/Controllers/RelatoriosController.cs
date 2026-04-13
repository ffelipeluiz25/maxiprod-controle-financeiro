using FinanceApp.Services.Interface;
using Microsoft.AspNetCore.Mvc;
namespace FinanceApp.Controllers
{
    [ApiController]
    [Route("api/relatorios")]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioService _relatorioService;

        public RelatoriosController(IRelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        [HttpGet("totais-por-pessoa")]
        public async Task<IActionResult> TotaisPorPessoa()
        {
            var result = await _relatorioService.TotaisPorPessoa();
            return Ok(result);
        }

        [HttpGet("totais-por-categoria")]
        public async Task<IActionResult> TotaisPorCategoria()
        {
            var result = await _relatorioService.TotaisPorCategoria();
            return Ok(result);
        }

    }
}

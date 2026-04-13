using FinanceApp.DTOs.Pessoa.Request;
using FinanceApp.Services.Interface;
using Microsoft.AspNetCore.Mvc;
namespace FinanceApp.Controllers
{
    [ApiController]
    [Route("api/pessoas")]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoasController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarPessoaDTO request)
        {
            var retornoWs = await _pessoaService.Criar(request);
            return Ok(retornoWs);
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var retornoWs = await _pessoaService.ListarTodas();
            return Ok(retornoWs);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, AtualizarPessoaDTO request)
        {
            if (id != request.Id)
                return BadRequest("Id inconsistente");

            var retornoWs = await _pessoaService.Atualizar(request);
            return Ok(retornoWs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var retornoWs = await _pessoaService.Deletar(id);
            return Ok(retornoWs);
        }

    }
}
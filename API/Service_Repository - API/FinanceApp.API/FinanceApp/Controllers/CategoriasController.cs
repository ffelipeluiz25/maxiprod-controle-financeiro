using FinanceApp.DTOs.Categoria.Request;
using FinanceApp.Services.Interface;
using Microsoft.AspNetCore.Mvc;
namespace FinanceApp.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarCategoriaDTO request)
        {
            var id = await _categoriaService.Criar(request);
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var result = await _categoriaService.Listar();
            return Ok(result);
        }

    }
}
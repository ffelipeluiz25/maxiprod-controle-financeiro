using FinanceApp.DTOs;
using FinanceApp.DTOs.Categoria;
using FinanceApp.DTOs.Categoria.Request;
namespace FinanceApp.Services.Interface
{
    public interface ICategoriaService
    {
        Task<RetornoDTO<int>> Criar(CriarCategoriaDTO request);
        Task<RetornoDTO<List<CategoriaDTO>>> Listar();
    }
}
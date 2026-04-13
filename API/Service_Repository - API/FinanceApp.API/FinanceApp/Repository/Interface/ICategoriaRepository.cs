using FinanceApp.DTOs;
using FinanceApp.DTOs.Categoria;
namespace FinanceApp.Repository.Interface
{
    public interface ICategoriaRepository
    {
        Task<RetornoDTO<int>> Add(CategoriaDTO categoria);
        Task<RetornoDTO<List<CategoriaDTO>>> ListarTodas();
        Task<CategoriaDTO> ObterPorId(int idCategoria);
    }
}
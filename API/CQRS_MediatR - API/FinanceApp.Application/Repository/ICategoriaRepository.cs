using FinanceApp.Application.DTOs;
using FinanceApp.Domain.Entidades;
namespace FinanceApp.Application.Repository
{
    public interface ICategoriaRepository
    {
        Task<RetornoDTO<int>> Add(CategoriaEntity categoria);
        Task<List<CategoriaEntity>> ListarTodas();
        Task<RetornoDTO<CategoriaEntity>> ObterPorId(int idCategoria);
    }
}
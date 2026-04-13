using FinanceApp.Application.DTOs;
using FinanceApp.Application.Features.Pessoas.Commands;
using FinanceApp.Domain.Entidades;
namespace FinanceApp.Application.Repository
{
    public interface IPessoaRepository
    {
        Task<RetornoDTO<int>> Add(PessoaEntity pessoa);
        Task<RetornoDTO<bool>> Delete(int id);
        Task<List<PessoaEntity>> ListarTodas();
        Task<RetornoDTO<PessoaEntity>> ObterPorId(int id);
        Task<RetornoDTO<bool>> Update(EditarPessoaCommand pessoa);
    }
}
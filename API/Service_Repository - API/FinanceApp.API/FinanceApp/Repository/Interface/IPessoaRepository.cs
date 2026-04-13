using FinanceApp.DTOs;
using FinanceApp.DTOs.Pessoa;
namespace FinanceApp.Repository.Interface
{
    public interface IPessoaRepository
    {
        Task<RetornoDTO<List<PessoaDTO>>> ListarTodas();
        Task<RetornoDTO<int>> Criar(PessoaDTO pessoa);
        Task<RetornoDTO<bool>> Atualizar(PessoaDTO pessoa);
        Task<RetornoDTO<bool>> Deletar(int id);
        Task<PessoaDTO> ObterPorId(int id);
    }
}
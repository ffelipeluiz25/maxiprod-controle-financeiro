using FinanceApp.DTOs;
using FinanceApp.DTOs.Pessoa;
using FinanceApp.DTOs.Pessoa.Request;
namespace FinanceApp.Services.Interface
{
    public interface IPessoaService
    {
        Task<RetornoDTO<List<PessoaDTO>>> ListarTodas();
        Task<RetornoDTO<int>> Criar(CriarPessoaDTO request);
        Task<RetornoDTO<bool>> Atualizar(AtualizarPessoaDTO request);
        Task<RetornoDTO<bool>> Deletar(int id);
    }
}
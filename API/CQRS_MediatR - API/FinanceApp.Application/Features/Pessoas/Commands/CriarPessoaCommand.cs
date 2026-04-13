using FinanceApp.Application.DTOs;
using MediatR;
namespace FinanceApp.Application.Features.Pessoas.Commands
{

    public record CriarPessoaCommand(string Nome, int Idade) : IRequest<RetornoDTO<int>>;
}
using FinanceApp.Application.DTOs;
using MediatR;
namespace FinanceApp.Application.Features.Pessoas.Commands
{
    public record EditarPessoaCommand(int Id, string Nome, int Idade) : IRequest<RetornoDTO<bool>>;
}
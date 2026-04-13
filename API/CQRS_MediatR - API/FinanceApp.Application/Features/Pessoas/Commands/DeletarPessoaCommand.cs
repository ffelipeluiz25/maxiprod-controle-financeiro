using FinanceApp.Application.DTOs;
using MediatR;
namespace FinanceApp.Application.Features.Pessoas.Commands
{
    public record DeletarPessoaCommand(int Id) : IRequest<RetornoDTO<bool>>;
}
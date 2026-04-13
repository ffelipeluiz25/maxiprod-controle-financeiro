using FinanceApp.Application.DTOs;
using FinanceApp.Application.DTOs.Pessoa;
using MediatR;
namespace FinanceApp.Application.Features.Pessoas.Queries
{
    public record ListarPessoasQuery() : IRequest<RetornoDTO<List<PessoaDTO>>>;
}
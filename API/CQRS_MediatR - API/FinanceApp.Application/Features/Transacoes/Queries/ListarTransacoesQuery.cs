using FinanceApp.Application.DTOs;
using FinanceApp.Application.DTOs.Transacao;
using MediatR;
namespace FinanceApp.Application.Features.Transacoes.Queries
{
    public record ListarTransacoesQuery() : IRequest<RetornoDTO<List<TransacaoDTO>>>;
}
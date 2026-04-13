using FinanceApp.Application.DTOs;
using FinanceApp.Application.DTOs.Relatorio.RelatorioPorPessoa;
using MediatR;
namespace FinanceApp.Application.Features.Relatorios.Queries
{
    public record TotaisPorPessoaQuery() : IRequest<RetornoDTO<RelatorioTotaisPorPessoaDTO>>;
}
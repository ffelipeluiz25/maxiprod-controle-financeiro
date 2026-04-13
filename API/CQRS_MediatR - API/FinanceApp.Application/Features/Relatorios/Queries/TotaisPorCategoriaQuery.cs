using FinanceApp.Application.DTOs;
using FinanceApp.Application.DTOs.Relatorio.RelatorioPorCategoria;
using MediatR;
namespace FinanceApp.Application.Features.Relatorios.Queries
{
    public record TotaisPorCategoriaQuery() : IRequest<RetornoDTO<RelatorioTotaisPorCategoriaDTO>>;
}
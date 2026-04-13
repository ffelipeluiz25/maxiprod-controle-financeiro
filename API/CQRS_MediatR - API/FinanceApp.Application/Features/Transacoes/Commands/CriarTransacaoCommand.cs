using FinanceApp.Application.DTOs;
using FinanceApp.Domain.Enumeradores;
using MediatR;
namespace FinanceApp.Application.Features.Transacoes.Commands
{
    public record CriarTransacaoCommand(string Descricao, decimal Valor, EnumTipoTransacao Tipo, int IdCategoria, int IdPessoa) : IRequest<RetornoDTO<int>>;
}
using FinanceApp.Application.DTOs;
using FinanceApp.Domain.Enumeradores;
using MediatR;
namespace FinanceApp.Application.Features.Categorias.Commands
{
    public record CriarCategoriaCommand(string Descricao, EnumFinalidadeCategoria EnumFinalidade) : IRequest<RetornoDTO<int>>;
}
using FinanceApp.Application.DTOs;
using FinanceApp.Application.DTOs.Categoria;
using MediatR;
namespace FinanceApp.Application.Features.Categorias.Queries
{
    public record ListarCategoriasQuery() : IRequest<RetornoDTO<List<CategoriaDTO>>>;
}
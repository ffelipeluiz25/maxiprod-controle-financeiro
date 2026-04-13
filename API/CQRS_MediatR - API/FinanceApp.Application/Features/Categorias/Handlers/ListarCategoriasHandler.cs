using AutoMapper;
using FinanceApp.Application.DTOs;
using FinanceApp.Application.DTOs.Categoria;
using FinanceApp.Application.Features.Categorias.Queries;
using FinanceApp.Application.Repository;
using FinanceApp.Domain.Entidades;
using MediatR;
namespace FinanceApp.Application.Features.Categorias.Handlers
{

    public class ListarCategoriasHandler : IRequestHandler<ListarCategoriasQuery, RetornoDTO<List<CategoriaDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaRepository _categoriaRepository;

        public ListarCategoriasHandler(IMapper mapper,
                                       ICategoriaRepository categoriaRepository)
        {
            _mapper = mapper;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<RetornoDTO<List<CategoriaDTO>>> Handle(ListarCategoriasQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var lista = await _categoriaRepository.ListarTodas();
                return new RetornoDTO<List<CategoriaDTO>>(true, _mapper.Map<List<CategoriaDTO>>(lista));
            }
            catch (Exception ex)
            {
                return new RetornoDTO<List<CategoriaDTO>>(false, ex.Message);
            }

        }

    }
}
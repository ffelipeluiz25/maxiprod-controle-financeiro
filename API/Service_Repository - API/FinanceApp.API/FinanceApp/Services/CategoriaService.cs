using AutoMapper;
using FinanceApp.DTOs;
using FinanceApp.DTOs.Categoria;
using FinanceApp.DTOs.Categoria.Request;
using FinanceApp.Repository.Interface;
using FinanceApp.Services.Interface;
namespace FinanceApp.Services
{
    public class CategoriaService : ICategoriaService
    {

        private readonly IMapper _mapper;
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(IMapper mapper,
                                ICategoriaRepository categoriaRepository)
        {
            _mapper = mapper;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<RetornoDTO<int>> Criar(CriarCategoriaDTO request)
        {
            var categoria = new CategoriaDTO
            {
                Descricao = request.Descricao,
                Finalidade = request.Finalidade,
                DataCriacao = DateTime.Now
            };

            return await _categoriaRepository.Add(categoria);
        }

        public async Task<RetornoDTO<List<CategoriaDTO>>> Listar()
        {
            return await _categoriaRepository.ListarTodas();
        }

    }
}
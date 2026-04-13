using AutoMapper;
using FinanceApp.Application.DTOs;
using FinanceApp.Application.DTOs.Pessoa;
using FinanceApp.Application.Features.Pessoas.Queries;
using FinanceApp.Application.Repository;
using MediatR;
namespace FinanceApp.Application.Features.Pessoas.Handlers
{
    public class ListarPessoasHandler : IRequestHandler<ListarPessoasQuery, RetornoDTO<List<PessoaDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IPessoaRepository _pessoaRepository;

        public ListarPessoasHandler(IMapper mapper,
                                    IPessoaRepository pessoaRepository)
        {
            _mapper = mapper;
            _pessoaRepository = pessoaRepository;
        }

        public async Task<RetornoDTO<List<PessoaDTO>>> Handle(ListarPessoasQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var lista = await _pessoaRepository.ListarTodas();
                return new RetornoDTO<List<PessoaDTO>>(true, _mapper.Map<List<PessoaDTO>>(lista));
            }
            catch (Exception ex)
            {
                return new RetornoDTO<List<PessoaDTO>>(false, ex.Message);
            }
        }

    }
}
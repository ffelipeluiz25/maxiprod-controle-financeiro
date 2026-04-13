using AutoMapper;
using FinanceApp.Application.DTOs;
using FinanceApp.Application.DTOs.Transacao;
using FinanceApp.Application.Features.Transacoes.Queries;
using FinanceApp.Application.Repository;
using MediatR;
namespace FinanceApp.Application.Features.Transacoes.Handlers
{
    public class ListarTransacoesHandler : IRequestHandler<ListarTransacoesQuery, RetornoDTO<List<TransacaoDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly ITransacaoRepository _transacaoRepository;

        public ListarTransacoesHandler(IMapper mapper,
                                       ITransacaoRepository transacaoRepository)
        {
            _mapper = mapper;
            _transacaoRepository = transacaoRepository;
        }

        public async Task<RetornoDTO<List<TransacaoDTO>>> Handle(ListarTransacoesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var lista = await _transacaoRepository.ListarTransacoes();
                return new RetornoDTO<List<TransacaoDTO>>(true, _mapper.Map<List<TransacaoDTO>>(lista));
            }
            catch (Exception ex)
            {
                return new RetornoDTO<List<TransacaoDTO>>(false, ex.Message);
            }
        }

    }
}
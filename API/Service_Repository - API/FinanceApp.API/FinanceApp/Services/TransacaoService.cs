using AutoMapper;
using FinanceApp.DTOs;
using FinanceApp.DTOs.Transacao;
using FinanceApp.DTOs.Transacao.Request;
using FinanceApp.Enumeradores;
using FinanceApp.Repository.Interface;
using FinanceApp.Services.Interface;
namespace FinanceApp.Services
{
    public class TransacaoService : ITransacaoService
    {

        private readonly IMapper _mapper;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ITransacaoRepository _transacaoRepository;

        public TransacaoService(IMapper mapper,
                                IPessoaRepository pessoaRepository,
                                ICategoriaRepository categoriaRepository,
                                ITransacaoRepository transacaoRepository)
        {
            _mapper = mapper;
            _pessoaRepository = pessoaRepository;
            _categoriaRepository = categoriaRepository;
            _transacaoRepository = transacaoRepository;
        }

        public async Task<RetornoDTO<int>> Criar(CriarTransacaoDTO request)
        {
            try
            {
                var pessoa = await _pessoaRepository.ObterPorId(request.IdPessoa);
                var categoria = await _categoriaRepository.ObterPorId(request.IdCategoria);

                if (pessoa == null || categoria == null)
                    return new RetornoDTO<int>(false, "Pessoa ou Categoria não encontrada");

                if (string.IsNullOrWhiteSpace(request.Descricao))
                    return new RetornoDTO<int>(false, "Descrição obrigatória");

                if (pessoa.Idade < 18 && request.Tipo == EnumTipoTransacao.Receita)
                    return new RetornoDTO<int>(false, "Menor não pode ter receita");

                if (request.Tipo == EnumTipoTransacao.Despesa && categoria.Finalidade == EnumFinalidadeCategoria.Receita)
                    return new RetornoDTO<int>(false, "Categoria inválida");

                if (request.Tipo == EnumTipoTransacao.Receita && categoria.Finalidade == EnumFinalidadeCategoria.Despesa)
                    return new RetornoDTO<int>(false, "Categoria inválida");

                var transacao = new TransacaoDTO
                {
                    Descricao = request.Descricao,
                    Valor = request.Valor,
                    Tipo = request.Tipo,
                    IdCategoria = request.IdCategoria,
                    IdPessoa = request.IdPessoa,
                    DataCriacao = DateTime.UtcNow
                };

                var IdTransacao = await _transacaoRepository.Add(transacao);
                return new RetornoDTO<int>(true, IdTransacao);
            }
            catch (Exception ex)
            {
                return new RetornoDTO<int>(false, ex.Message);
            }
        }

        public async Task<RetornoDTO<List<TransacaoDTO>>> ListarTransacoes()
        {
            try
            {
                var lista = await _transacaoRepository.ListarTransacoes();
                return new RetornoDTO<List<TransacaoDTO>>(true, lista);
            }
            catch (Exception ex)
            {
                return new RetornoDTO<List<TransacaoDTO>>(false, ex.Message);
            }
        }

    }
}
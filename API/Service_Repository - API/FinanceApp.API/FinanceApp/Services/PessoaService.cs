using AutoMapper;
using FinanceApp.DTOs;
using FinanceApp.DTOs.Pessoa;
using FinanceApp.DTOs.Pessoa.Request;
using FinanceApp.Repository.Interface;
using FinanceApp.Services.Interface;
namespace FinanceApp.Services
{
    public class PessoaService : IPessoaService
    {

        private readonly IMapper _mapper;
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaService(IMapper mapper,
                             IPessoaRepository pessoaRepository)
        {
            _mapper = mapper;
            _pessoaRepository = pessoaRepository;
        }

        public async Task<RetornoDTO<bool>> Atualizar(AtualizarPessoaDTO request)
        {
            return await _pessoaRepository.Atualizar(_mapper.Map<PessoaDTO>(request));
        }

        public async Task<RetornoDTO<int>> Criar(CriarPessoaDTO request)
        {
            if (request.Idade.Equals(0))
                return new RetornoDTO<int>(false, "Idade deve ser preenchida e ser maior que zero!");

            return await _pessoaRepository.Criar(_mapper.Map<PessoaDTO>(request));
        }

        public async Task<RetornoDTO<bool>> Deletar(int id)
        {
            return await _pessoaRepository.Deletar(id);
        }

        public async Task<RetornoDTO<List<PessoaDTO>>> ListarTodas()
        {
            return await _pessoaRepository.ListarTodas();
        }
    }
}
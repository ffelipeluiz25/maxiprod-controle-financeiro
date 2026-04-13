using AutoMapper;
using FinanceApp.DbContexts;
using FinanceApp.Entity;
using FinanceApp.DTOs.Transacao;
using FinanceApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
namespace FinanceApp.Repository
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public TransacaoRepository(IMapper mapper, AppDbContext context, IConfiguration configuration)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> Add(TransacaoDTO transacao)
        {
            var entity = _mapper.Map<TransacaoEntity>(transacao);
            _context.Transacoes.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<List<TransacaoDTO>> ListarTransacoes()
        {
            var transacao = await (from t in _context.Transacoes
                                   join p in _context.Pessoas on t.IdPessoa equals p.Id
                                   join c in _context.Categorias on t.IdCategoria equals c.Id
                                   select new TransacaoDTO()
                                   {
                                       Id = t.Id,
                                       DataCriacao = t.DataCriacao,
                                       DataAlteracao = t.DataAlteracao,
                                       Descricao = t.Descricao,
                                       Valor = t.Valor,
                                       Tipo = t.Tipo,
                                       IdCategoria = t.IdCategoria,
                                       IdPessoa = t.IdPessoa,
                                       CategoriaNome = c.Descricao,
                                       PessoaNome = p.Nome,
                                       PessoaIdade = p.Idade
                                   }).ToListAsync();

            return transacao;
        }

    }
}
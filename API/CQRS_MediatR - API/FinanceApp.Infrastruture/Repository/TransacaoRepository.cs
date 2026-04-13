using FinanceApp.Application.DTOs;
using FinanceApp.Application.DTOs.Transacao;
using FinanceApp.Application.Repository;
using FinanceApp.Domain.Entidades;
using FinanceApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace FinanceApp.Infrastructure.Repository
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly AppDbContext _context;

        public TransacaoRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
        }

        public async Task<RetornoDTO<int>> Add(TransacaoEntity transacao)
        {
            try
            {
                _context.Transacoes.Add(transacao);
                await _context.SaveChangesAsync();
                return new RetornoDTO<int>(true, transacao.Id);
            }
            catch (Exception ex)
            {
                return new RetornoDTO<int>(false, ex.Message);
            }
        }

        public async Task<List<TransacaoDTO>> ListarTransacoes()
        {
            var lista = await (from t in _context.Transacoes
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
            return lista;
        }

    }
}
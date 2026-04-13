using Dapper;
using FinanceApp.Application.DTOs;
using FinanceApp.Application.Features.Pessoas.Commands;
using FinanceApp.Application.Repository;
using FinanceApp.Domain.Entidades;
using FinanceApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
namespace FinanceApp.Infrastructure.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public PessoaRepository(AppDbContext context,
                                IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<RetornoDTO<int>> Add(PessoaEntity pessoa)
        {
            try
            {
                _context.Pessoas.Add(pessoa);
                await _context.SaveChangesAsync();
                return new RetornoDTO<int>(true, pessoa.Id);
            }
            catch (Exception ex)
            {
                return new RetornoDTO<int>(false, ex.Message);
            }
        }

        public async Task<RetornoDTO<bool>> Delete(int id)
        {
            var pessoaBD = await _context.Pessoas.FindAsync(id);
            if (pessoaBD == null)
                return new RetornoDTO<bool>(false, "Pessoa não encontrada!");

            try
            {
                _context.Pessoas.Remove(pessoaBD);
                await _context.SaveChangesAsync();
                return new RetornoDTO<bool>(true, true);
            }
            catch (Exception ex)
            {
                return new RetornoDTO<bool>(false, ex.Message);
            }
        }

        public async Task<List<PessoaEntity>> ListarTodas()
        {
            var pessoas = await _context.Pessoas
                                        .Include(t => t.Transacoes)
                                        .Select(p => new PessoaEntity
                                        {
                                            Id = p.Id,
                                            Nome = p.Nome,
                                            Idade = p.Idade,
                                            DataCriacao = p.DataCriacao,
                                            DataAlteracao = p.DataAlteracao,
                                            Transacoes = p.Transacoes.Select(t => new TransacaoEntity
                                            {
                                                Id = t.Id,
                                                Descricao = t.Descricao,
                                                Valor = t.Valor,
                                                Tipo = t.Tipo,
                                                IdCategoria = t.IdCategoria,
                                                IdPessoa = t.IdPessoa,
                                                DataCriacao = t.DataCriacao,
                                                DataAlteracao = t.DataAlteracao,
                                            }).ToList()
                                        }).ToListAsync();


            return pessoas;
        }

        public async Task<RetornoDTO<PessoaEntity>> ObterPorId(int id)
        {
            try
            {
                using var conn = new MySqlConnection(_configuration["ConnectionStrings:Default"]);
                var sql = @"SELECT Id, Nome, Idade, DataCriacao, DataAlteracao
                        FROM Pessoas
                        WHERE Id = @Id";
                var pessoa = await conn.QueryFirstOrDefaultAsync<PessoaEntity>(sql, new { Id = id });
                return new RetornoDTO<PessoaEntity>(true, pessoa);
            }
            catch (Exception ex)
            {
                return new RetornoDTO<PessoaEntity>(false, ex.Message);
            }
        }

        public async Task<RetornoDTO<bool>> Update(EditarPessoaCommand pessoaAtualizada)
        {
            var pessoa = await _context.Pessoas.FindAsync(pessoaAtualizada.Id);
            if (pessoa == null)
                return new RetornoDTO<bool>(false, "Pessoa não encontrada!");

            try
            {
                pessoa.Nome = pessoaAtualizada.Nome;
                pessoa.Idade = pessoaAtualizada.Idade;
                pessoa.DataAlteracao = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return new RetornoDTO<bool>(true, true);
            }
            catch (Exception ex)
            {
                return new RetornoDTO<bool>(false, ex.Message);
            }
        }

    }
}
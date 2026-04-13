using AutoMapper;
using Dapper;
using FinanceApp.DbContexts;
using FinanceApp.DTOs;
using FinanceApp.DTOs.Pessoa;
using FinanceApp.DTOs.Transacao;
using FinanceApp.Entity;
using FinanceApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
namespace FinanceApp.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public PessoaRepository(IMapper mapper,
                                AppDbContext context,
                                IConfiguration configuration)
        {
            _mapper = mapper;
            _context = context;
            _configuration = configuration;
        }

        public async Task<RetornoDTO<List<PessoaDTO>>> ListarTodas()
        {
            try
            {
                var pessoas = await _context.Pessoas
                                    .Include(t => t.Transacoes)
                                    .Select(p => new PessoaDTO
                                    {
                                        Id = p.Id,
                                        Nome = p.Nome,
                                        Idade = p.Idade,
                                        DataCriacao = p.DataCriacao,
                                        DataAlteracao = p.DataAlteracao,
                                        Transacoes = p.Transacoes.Select(t => new TransacaoDTO
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

                return new RetornoDTO<List<PessoaDTO>>(true, pessoas);
            }
            catch (Exception ex)
            {
                return new RetornoDTO<List<PessoaDTO>>(false, ex.Message);
            }

        }

        public async Task<RetornoDTO<int>> Criar(PessoaDTO pessoa)
        {
            try
            {
                var entity = _mapper.Map<PessoaEntity>(pessoa);

                _context.Pessoas.Add(entity);
                await _context.SaveChangesAsync();

                return new RetornoDTO<int>(true, entity.Id);
            }
            catch (Exception ex)
            {
                return new RetornoDTO<int>(false, ex.Message);
            }
        }

        public async Task<RetornoDTO<bool>> Atualizar(PessoaDTO pessoaAtualizada)
        {
            try
            {
                var pessoa = await _context.Pessoas.FindAsync(pessoaAtualizada.Id);
                if (pessoa == null)
                    return new RetornoDTO<bool>(false, "Pessoa não encontrada!");

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

        public async Task<RetornoDTO<bool>> Deletar(int id)
        {
            try
            {
                var pessoaBD = await _context.Pessoas.FindAsync(id);
                if (pessoaBD == null)
                    return new RetornoDTO<bool>(false, "Pessoa não encontrada!");

                _context.Pessoas.Remove(pessoaBD);
                await _context.SaveChangesAsync();
                return new RetornoDTO<bool>(true, true);

            }
            catch (Exception ex)
            {
                return new RetornoDTO<bool>(false, ex.Message);
            }

        }

        public async Task<PessoaDTO> ObterPorId(int id)
        {
            using var conn = new MySqlConnection(_configuration["ConnectionStrings:Default"]);
            var sql = @"SELECT 
                            Id, 
                            Nome, 
                            Idade, 
                            DataCriacao, 
                            DataAlteracao
                        FROM 
                            Pessoas
                        WHERE 
                            Id = @Id";

            var pessoa = await conn.QueryFirstOrDefaultAsync<PessoaDTO>(sql, new { Id = id });
            if (pessoa == null)
                return new PessoaDTO();

            return pessoa;
        }

    }
}
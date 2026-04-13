using AutoMapper;
using Dapper;
using FinanceApp.DbContexts;
using FinanceApp.DTOs;
using FinanceApp.DTOs.Categoria;
using FinanceApp.Entity;
using FinanceApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
namespace FinanceApp.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public CategoriaRepository(IMapper mapper, AppDbContext context, IConfiguration configuration)
        {
            _mapper = mapper;
            _context = context;
            _configuration = configuration;
        }

        public async Task<RetornoDTO<int>> Add(CategoriaDTO categoria)
        {
            try
            {

                _context.Categorias.Add(_mapper.Map<CategoriaEntity>(categoria));
                await _context.SaveChangesAsync();
                return new RetornoDTO<int>(true, categoria.Id);
            }
            catch (Exception ex)
            {
                return new RetornoDTO<int>(false, ex.Message);
            }
        }

        public async Task<RetornoDTO<List<CategoriaDTO>>> ListarTodas()
        {
            try
            {
                var lista = _mapper.Map<List<CategoriaDTO>>(await _context.Categorias.ToListAsync());
                return new RetornoDTO<List<CategoriaDTO>>(true, lista);
            }
            catch (Exception ex)
            {
                return new RetornoDTO<List<CategoriaDTO>>(false, ex.Message);
            }

        }

        public async Task<CategoriaDTO> ObterPorId(int idCategoria)
        {
            using var conn = new MySqlConnection(_configuration["ConnectionStrings:Default"]);
            var sql = @"SELECT 
                            Id, 
                            Descricao, 
                            Finalidade, 
                            DataCriacao, 
                            DataAlteracao
                        FROM 
                            Categorias
                        WHERE 
                            Id = @Id";

            var categoria = await conn.QueryFirstOrDefaultAsync<CategoriaDTO>(sql, new { Id = idCategoria });
            if (categoria == null)
                return new CategoriaDTO();

            return categoria;
        }

    }
}
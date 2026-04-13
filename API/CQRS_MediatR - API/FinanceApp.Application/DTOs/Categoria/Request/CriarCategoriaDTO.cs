using FinanceApp.Domain.Enumeradores;
namespace FinanceApp.Application.DTOs.Categoria.Request
{
    public class CriarCategoriaDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public EnumFinalidadeCategoria Finalidade { get; set; }
    }
}
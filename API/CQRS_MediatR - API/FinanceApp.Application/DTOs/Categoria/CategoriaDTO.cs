using FinanceApp.Domain.Enumeradores;
namespace FinanceApp.Application.DTOs.Categoria
{
    public class CategoriaDTO : BaseDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public EnumFinalidadeCategoria Finalidade { get; set; }
    }
}
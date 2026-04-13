using FinanceApp.Enumeradores;
namespace FinanceApp.Entity
{
    public class CategoriaEntity : BaseEntity
    {
        public string Descricao { get; set; } = string.Empty;
        public EnumFinalidadeCategoria Finalidade { get; set; }
    }
}
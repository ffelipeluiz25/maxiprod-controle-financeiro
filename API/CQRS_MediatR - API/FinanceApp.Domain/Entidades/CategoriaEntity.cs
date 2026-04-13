using FinanceApp.Domain.Enumeradores;
namespace FinanceApp.Domain.Entidades
{
    public class CategoriaEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public EnumFinalidadeCategoria Finalidade { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
using FinanceApp.Entity;
namespace FinanceApp.Entity
{
    public class PessoaEntity : BaseEntity
    {
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        public List<TransacaoEntity> Transacoes { get; set; } = new();
    }
}
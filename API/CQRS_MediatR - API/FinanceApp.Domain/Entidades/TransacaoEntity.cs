using FinanceApp.Domain.Enumeradores;
namespace FinanceApp.Domain.Entidades
{
    public class TransacaoEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public EnumTipoTransacao Tipo { get; set; }
        public int IdCategoria { get; set; }
        public int IdPessoa { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
using FinanceApp.Entity;
using FinanceApp.Enumeradores;
using System.ComponentModel.DataAnnotations.Schema;
namespace FinanceApp.Entity
{
    public class TransacaoEntity : BaseEntity
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public EnumTipoTransacao Tipo { get; set; }
        public int IdCategoria { get; set; }
        public int IdPessoa { get; set; }

        [ForeignKey("IdPessoa")]
        public PessoaEntity Pessoa { get; set; }

        [ForeignKey("IdCategoria")]
        public CategoriaEntity Categoria { get; set; }
    }
}
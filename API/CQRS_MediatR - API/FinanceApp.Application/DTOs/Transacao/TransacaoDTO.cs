using FinanceApp.Domain.Enumeradores;
namespace FinanceApp.Application.DTOs.Transacao
{
    public class TransacaoDTO : BaseDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public EnumTipoTransacao Tipo { get; set; }
        public int IdCategoria { get; set; }
        public int IdPessoa { get; set; }

        //Custom
        public string PessoaNome { get; set; }
        public string CategoriaNome { get; set; }
        public int PessoaIdade { get; set; }
    }
}
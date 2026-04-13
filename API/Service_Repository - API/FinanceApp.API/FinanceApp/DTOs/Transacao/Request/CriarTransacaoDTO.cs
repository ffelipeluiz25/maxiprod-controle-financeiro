using FinanceApp.Enumeradores;
namespace FinanceApp.DTOs.Transacao.Request
{
    public class CriarTransacaoDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public EnumTipoTransacao Tipo { get; set; }
        public int IdCategoria { get; set; }
        public int IdPessoa { get; set; }
    }

}
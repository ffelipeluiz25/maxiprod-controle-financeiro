using FinanceApp.Application.DTOs.Transacao;
namespace FinanceApp.Application.DTOs.Pessoa
{
    public class PessoaDTO : BaseDTO
    {
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        public List<TransacaoDTO> Transacoes { get; set; } = new();
    }
}
using FinanceApp.DTOs.Transacao;
namespace FinanceApp.DTOs.Pessoa
{
    public class PessoaDTO : BaseDTO
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public List<TransacaoDTO> Transacoes { get; set; } 
    }
}
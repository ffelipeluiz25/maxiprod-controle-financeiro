namespace FinanceApp.DTOs.Pessoa.Request
{
    public class AtualizarPessoaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
    }
}
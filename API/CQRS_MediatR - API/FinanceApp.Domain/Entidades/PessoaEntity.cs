namespace FinanceApp.Domain.Entidades
{
    public class PessoaEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public List<TransacaoEntity> Transacoes { get; set; } = new();
    }
}
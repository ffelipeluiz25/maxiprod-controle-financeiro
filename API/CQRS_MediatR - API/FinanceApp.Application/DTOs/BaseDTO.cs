namespace FinanceApp.Application.DTOs
{
    public class BaseDTO
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
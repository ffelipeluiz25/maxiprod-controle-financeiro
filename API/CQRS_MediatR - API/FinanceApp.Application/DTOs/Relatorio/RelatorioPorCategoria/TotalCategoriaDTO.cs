namespace FinanceApp.Application.DTOs.Relatorio.RelatorioPorCategoria
{
    public class TotalCategoriaDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal Saldo { get; set; }
    }
}
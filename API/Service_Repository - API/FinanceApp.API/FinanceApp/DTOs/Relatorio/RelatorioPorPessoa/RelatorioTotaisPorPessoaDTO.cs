namespace FinanceApp.DTOs.Relatorio.RelatorioPorPessoa
{
    public class RelatorioTotaisPorPessoaDTO
    {
        public RelatorioTotaisPorPessoaDTO(List<TotalPessoaDTO> _listaTotalPessoas, TotalGeralDTO _totalGeral)
        {
            ListaTotalPessoas = _listaTotalPessoas;
            TotalGeral = _totalGeral;
        }
        public List<TotalPessoaDTO> ListaTotalPessoas { get; set; }
        public TotalGeralDTO TotalGeral { get; set; }
    }
}
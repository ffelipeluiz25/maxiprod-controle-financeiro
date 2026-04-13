namespace FinanceApp.DTOs.Relatorio.RelatorioPorCategoria
{
    public class RelatorioTotaisPorCategoriaDTO
    {
        public RelatorioTotaisPorCategoriaDTO(List<TotalCategoriaDTO> _listaTotalCategorias, TotalGeralDTO _totalGeral)
        {
            ListaTotalCategorias = _listaTotalCategorias;
            TotalGeral = _totalGeral;
        }
        public List<TotalCategoriaDTO> ListaTotalCategorias { get; set; }
        public TotalGeralDTO TotalGeral { get; set; }
    }
}
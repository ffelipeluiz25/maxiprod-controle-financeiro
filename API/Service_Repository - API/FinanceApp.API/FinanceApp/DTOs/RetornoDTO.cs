namespace FinanceApp.DTOs
{
    public class RetornoDTO
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public string IdMensagem { get; set; }

        public RetornoDTO()
        {
        }

        public RetornoDTO(bool sucesso)
        {
            Sucesso = sucesso;
        }

        public RetornoDTO(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }
    }

    public class RetornoDTO<T> : RetornoDTO
    {
        public T Dados { get; set; }

        public RetornoDTO(bool sucesso, string mensagem, T dados) : base(sucesso, mensagem)
        {
            Dados = dados;
        }

        public RetornoDTO(bool sucesso, T dados) : base(sucesso)
        {
            Dados = dados;
        }

        public RetornoDTO(bool sucesso, string mensagem) : base(sucesso, mensagem)
        {
        }
    }
}
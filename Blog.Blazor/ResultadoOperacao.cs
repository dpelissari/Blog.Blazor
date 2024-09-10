namespace Blog.Blazor
{
    public class ResultadoOperacao
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public string Parametro { get; set; } = string.Empty;
    }
}

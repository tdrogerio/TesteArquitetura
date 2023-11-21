namespace TesteArquitetura.Documentos.Domain
{
    public class Log : Entity
    {
        public string Dominio { get; set; }
        public Guid IdDominio { get; set; }        
        public string Acao { get; set; }
        public string Message { get; set; }
        public string JsonObjectException { get; set; }
    }
}

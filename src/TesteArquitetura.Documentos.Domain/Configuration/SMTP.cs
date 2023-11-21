namespace TesteArquitetura.Documentos.Domain.Configuration
{
    public class SMTP
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string EnableSsl { get; set; }
        public string UseDefaultCredentials { get; set; }
        public string CredentialsUser { get; set; }
        public string CredentialsPass { get; set; }
    }
}

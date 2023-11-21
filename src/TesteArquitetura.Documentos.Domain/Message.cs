namespace TesteArquitetura.Documentos.Domain
{
    public class Message
    {
        public bool IsValid { get; set; }
        public int TypeMsg { get; set; }
        public string Msg { get; set; }
    }

    public enum MessageType
    {
       MsgError = 1,
       MsgWarning = 2,
       MsgInfo = 3,
       MsgSuccess = 4
    }

}
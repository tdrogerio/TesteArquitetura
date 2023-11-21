using TesteArquitetura.Documentos.Domain;

namespace TesteArquitetura.Documentos.Application.Services
{
    public static class MessageAppService
    {
        public static Message TrataMensagem(bool isValid, string msg, MessageType type)
        {
            return new Message()
            {
                IsValid = isValid,
                Msg = msg,
                TypeMsg = Convert.ToInt16(type)
            };
        }
    }
}

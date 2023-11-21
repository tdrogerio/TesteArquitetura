using TesteArquitetura.Documentos.Domain;

namespace TesteArquitetura.Documentos.Domain
{
    public class FluxoCaixa : Entity
    {
        public string Nome { get; set; }
        public int Codigo { get; set; }

        protected FluxoCaixa() { }
    }
}

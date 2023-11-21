using TesteArquitetura.Documentos.Domain;
using System.ComponentModel.DataAnnotations;

namespace TesteArquitetura.Documentos.Application.ViewModel
{
    public class FluxoCaixaViewModel : EntityViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Codigo { get; set; }
    }
}

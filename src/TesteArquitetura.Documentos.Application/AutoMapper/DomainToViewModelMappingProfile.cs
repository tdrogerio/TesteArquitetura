using AutoMapper;
using TesteArquitetura.Documentos.Application.ViewModel;
using TesteArquitetura.Documentos.Domain;

namespace TesteArquitetura.Documentos.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<FluxoCaixa, FluxoCaixaViewModel>();
        }
    }
}

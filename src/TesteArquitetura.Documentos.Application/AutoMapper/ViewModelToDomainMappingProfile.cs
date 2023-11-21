using AutoMapper;
using TesteArquitetura.Documentos.Application.ViewModel;
using TesteArquitetura.Documentos.Domain;

namespace TesteArquitetura.Documentos.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<FluxoCaixaViewModel, FluxoCaixa>();
        }
    }
}

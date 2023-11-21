using AutoMapper;
using TesteArquitetura.Documentos.Application.Services.Interfaces;
using TesteArquitetura.Documentos.Application.ViewModel;
using TesteArquitetura.Documentos.Domain;
using TesteArquitetura.Documentos.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace TesteArquitetura.Documentos.Application.Services
{
    public class FluxoCaixaAppService : Service<FluxoCaixa, FluxoCaixaViewModel>, IFluxoCaixaAppService
    {
        private readonly IFluxoCaixaRepository _repository;
        private readonly ILogger<Service<FluxoCaixa, FluxoCaixaViewModel>> _logger;
        private readonly IMapper _mapper;

        public FluxoCaixaAppService(IFluxoCaixaRepository repository, ILogger<Service<FluxoCaixa, FluxoCaixaViewModel>> logger, IMapper mapper)
            : base(repository, logger, mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
    }
}

using AutoMapper;
using TesteArquitetura.Documentos.Domain;
using TesteArquitetura.Documentos.Application.Services.Interfaces;
using TesteArquitetura.Documentos.Domain.Repositories;
using TesteArquitetura.Documentos.Domain.Repositories.Views;
using Microsoft.Extensions.Logging;

namespace TesteArquitetura.Documentos.Application.Services.Views
{
    public class VwService<TEntity, TViewModel> : IVWService<TEntity, TViewModel>
        where TViewModel : EntityViewModel where TEntity : Entity
    {
        private readonly IVWRepository<TEntity> _repository;
        private readonly ILogger<VwService<TEntity, TViewModel>> _logger;
        private readonly IMapper _mapper;

        public VwService(IVWRepository<TEntity> repository, ILogger<VwService<TEntity, TViewModel>> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<TViewModel> GetViewResult(string viewName)
        {
            var view = _repository.GetViewResult(viewName);
            return _mapper.Map<IEnumerable<TViewModel>>(view);
        }
    }
}

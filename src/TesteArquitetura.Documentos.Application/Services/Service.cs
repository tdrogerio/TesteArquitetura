using AutoMapper;
using TesteArquitetura.Documentos.Domain;
using TesteArquitetura.Documentos.Application.Services.Interfaces;
using TesteArquitetura.Documentos.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace TesteArquitetura.Documentos.Application.Services
{
    public class Service<TEntity, TViewModel> : IService<TEntity, TViewModel>
        where TViewModel : EntityViewModel where TEntity : Entity
    {
        private readonly IRepository<TEntity> _repository;
        private readonly ILogger<Service<TEntity, TViewModel>> _logger;
        private readonly IMapper _mapper;

        public Service(IRepository<TEntity> repository, ILogger<Service<TEntity, TViewModel>> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<TViewModel>>(_repository.AsQueryable().Where(p => p.IsAtivo == true));
        }

        public async Task<TViewModel> GetByIdAsync(Guid id)
        {
            var entity = _repository.AsQueryable().Where(p => p.Id == id).FirstOrDefault();
            return _mapper.Map<TViewModel>(entity);
        }

        public async Task RemoveAsync(TViewModel obj)
        {
            if (obj == null) 
            {
                _logger.LogInformation("Registro nulo.");
            }

            try
            {
                var entity = _mapper.Map<TEntity>(obj);
                _repository.Delete(entity);
                
                await _repository.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw new ArgumentNullException(ex.Message);
            }
        }

        public async Task<TViewModel> SaveAsync(TViewModel obj)
        {
            TViewModel vm = obj;
            Message msg;
            if (obj == null)
            {
                _logger.LogInformation("Registro nulo.");
                vm.message = MessageAppService.TrataMensagem(false, "Registro nulo.", MessageType.MsgError);
                return vm;
            }

            try
            {
                var entity = _mapper.Map<TEntity>(obj);
                entity = await _repository.SaveAsync(entity);
                return _mapper.Map<TViewModel>(entity);

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                vm.message = MessageAppService.TrataMensagem(false, ex.InnerException.ToString(), MessageType.MsgError);
                return vm;
            }
        }

        public async Task<TViewModel> UpdateAsync(TViewModel obj)
        {
            TViewModel vm = obj;
            Message msg;
            if (obj == null)
            {
                _logger.LogInformation("Registro nulo.");
                vm.message = MessageAppService.TrataMensagem(false, "Registro nulo.", MessageType.MsgError);
                return vm;
            }

            try
            {
                var entity = _mapper.Map<TEntity>(obj);
                entity = await _repository.UpdateAsync(entity);
                return _mapper.Map<TViewModel>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                vm.message = MessageAppService.TrataMensagem(false, ex.Message, MessageType.MsgError);
                return vm;
            }
        }

        
    }
}

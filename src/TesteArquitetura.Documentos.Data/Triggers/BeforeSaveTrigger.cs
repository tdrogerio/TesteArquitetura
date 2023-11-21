using EntityFrameworkCore.Triggered;
using TesteArquitetura.Documentos.Domain;

namespace TesteArquitetura.Documentos.Data.Triggers
{
    public class BeforeSaveTrigger : IBeforeSaveTrigger<Entity>
    {
        public Task BeforeSave(ITriggerContext<Entity> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added)
            {
                context.Entity.DataCadastro = DateTime.Now;
                context.Entity.DataAlteracao = null;
            }

            if (context.ChangeType == ChangeType.Modified)
            {
                context.Entity.DataAlteracao = DateTime.Now;
            }

            return Task.CompletedTask; throw new NotImplementedException();
        }
    }
}

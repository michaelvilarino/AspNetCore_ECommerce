using System.Threading.Tasks;
using EcMic.Core.DomainObjects.Data;
using FluentValidation.Results;

namespace EcMic.Core.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult validationResult;

        public CommandHandler()
        {
            validationResult = new ValidationResult();
        }

        protected void AdicionarErro(string mensagem)
        {
            validationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected async Task<ValidationResult> PersistirDados(IUnitOfWork uow)
        {
            if (!await uow.Commit()) AdicionarErro("Ocorreu um erro ao salvar");
            
            return validationResult;
        }
    }
}
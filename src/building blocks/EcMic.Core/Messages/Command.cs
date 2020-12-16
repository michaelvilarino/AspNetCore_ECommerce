using System;
using FluentValidation.Results;
using MediatR;

namespace EcMic.Core.Messages
{
    public abstract class Command: Message, IRequest<ValidationResult>
    {
        public DateTime TimeStamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        public Command()
        {
            TimeStamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
            
    }
}
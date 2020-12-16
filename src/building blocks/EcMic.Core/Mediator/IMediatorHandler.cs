using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EcMic.Core.Messages;
using FluentValidation.Results;

namespace EcMic.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T: Event;
        Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
    }
}

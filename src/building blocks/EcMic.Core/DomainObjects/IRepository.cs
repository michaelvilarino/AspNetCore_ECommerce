using System;
using System.Collections.Generic;
using System.Text;
using EcMic.Core.DomainObjects.Data;

namespace EcMic.Core.DomainObjects
{
    public interface IRepository<T> : IDisposable where T: IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}

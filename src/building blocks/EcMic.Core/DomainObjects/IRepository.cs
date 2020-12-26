using System;
using EcMic.Core.DomainObjects.Data;

namespace EcMic.Core.DomainObjects
{
    public interface IRepository<T> : IDisposable where T: IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}

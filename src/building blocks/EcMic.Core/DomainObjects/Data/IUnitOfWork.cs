using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcMic.Core.DomainObjects.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}

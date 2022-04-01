using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceProject.Business.IUnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }
}
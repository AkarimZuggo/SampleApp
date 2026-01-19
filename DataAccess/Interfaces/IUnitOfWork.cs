using DataAccess.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepository CompanyRepository { get; }
        Task<int> CommitAsync();
        Task CommitTransactionAsync();
        Task BeginTransactionAsync();
        Task RollbackTransactionAsync();
    }
}

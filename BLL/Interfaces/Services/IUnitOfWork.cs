using System;
using System.Threading;
using System.Threading.Tasks;
using BLL.Interfaces.Repositories;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Primitives;

namespace BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        Task<int> CompleteAsync(CancellationToken cancellationToken);

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);

        Task CommitTransactionAsync(CancellationToken cancellationToken);

        Task RollbackTransactionAsync(CancellationToken cancellationToken);

    }
}
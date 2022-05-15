using System;
using System.Threading.Tasks;
using BLL.Interfaces.Repositories;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable 
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

         Task<int> Complete();

        Task <IDbContextTransaction> BeginTransactionAsync();

         Task CommitTransaction();

         Task RollbackTransaction();

    }
}
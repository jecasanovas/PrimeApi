using BLL.Interfaces;
using BLL.Interfaces.Repositories;
using Core.DBContext;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Reposititories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GestionCursosContext _context;
        private Hashtable _repositories;

        private IDbContextTransaction _transaction;
        public UnitOfWork(GestionCursosContext context)
        {
            _context = context;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            await _transaction.CommitAsync(cancellationToken);
        }


        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            await _transaction.RollbackAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }
    }
}
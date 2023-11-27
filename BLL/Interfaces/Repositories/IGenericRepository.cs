using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BLL.Specification;
using Core.Entities;

namespace BLL.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken);
        Task<T> GetEntityWithSpec(ISpecification<T> spec, CancellationToken cancellationToken);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken);
        Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken);
        Task<int> CountAsync(CancellationToken cancellationToken);

        void Add(T entity);

        void AddRange(IEnumerable<T> entityList);

        void Update(T entity);
        void Delete(T entity);

        Task SaveChangesAsync();


    }
}
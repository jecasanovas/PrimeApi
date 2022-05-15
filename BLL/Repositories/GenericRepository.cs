using BLL.Interfaces;
using BLL.Interfaces.Repositories;
using BLL.Specification;
using Core.DBContext;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  BLL.Reposititories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly GestionCursosContext _context;

        public GenericRepository(GestionCursosContext context)
        {

            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {

            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {

            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {

            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();

        }


        private IQueryable<T> ApplySpecification(ISpecification<T> spec, string queryresult = "")
        {
            var query = SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
            queryresult = query.ToQueryString();

            return query;



        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }


        public void AddRange(IEnumerable<T> entityList)
        {
            _context.Set<T>().AddRange(entityList);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();

        }

    }
}
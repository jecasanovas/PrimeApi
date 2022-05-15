using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL.Specification
{
    public abstract class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        //Aqui esta construyendo el IQueriable  
        public int Id { get; set; }
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }


            if (spec.FieldsOrderBy != null)
            {
                 query = spec.FieldsOrderBy.Aggregate(query, (current, order) => current.OrderBy(order));
            }


            if (spec.FieldsOrderByDesc != null)
            {
                query = spec.FieldsOrderByDesc.Aggregate(query, (current, order) => current.OrderByDescending(order));
            }


            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            if (spec.Includes.Count > 0)
            {
                query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            }



            if (spec.Fields != null && spec.Fields.Count > 0)
            {
                var result = QueryableExtensions.GetReturningFields(spec.Fields);

                if (result.Count > 0)
                {
                    query = query.SelectMembers(result.ToArray());
                }
            }

            return query;
        }
    }
}
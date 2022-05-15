using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL.Specification
{
    public interface ISpecification<T>
    {
        List<Expression<Func<T, object>>> Fields { get; set; }
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }

        Expression<Func<T, object>>[] FieldsOrderBy { get; }

        Expression<Func<T, object>>[] FieldsOrderByDesc { get; }


        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}
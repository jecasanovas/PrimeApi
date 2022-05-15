using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BLL.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        

        public int PageIndex { get; set; }

        public Expression<Func<T, object>>[] FieldsOrderBy { get; set; }

        public Expression<Func<T, object>>[] FieldsOrderByDesc { get; set; }

        protected void FieldsAddOrderBy(Expression<Func<T, object>>[] orderByExpression)
        {
            FieldsOrderBy = orderByExpression;
        }

        protected void FieldsAddOrderByDesc(Expression<Func<T, object>>[] orderByExpression)
        {
            FieldsOrderByDesc = orderByExpression;
        }


        public int PageSize { get; set; }

        public Boolean OnlyCount { get; set; }

        public BaseSpecification(){}

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public void AddCondition(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;

        }




        public List<Expression<Func<T, object>>> Fields { get; set; } =
            new List<Expression<Func<T, object>>>();


        public Expression<Func<T, bool>> Criteria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; } =
            new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }



        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }


        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

 

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }



        public void ApplyPaging(int? skip, int? take)
        {
            
            if (!OnlyCount)
            {
                Skip = skip ?? 0;
                Take = take ?? 0;
                IsPagingEnabled = (take > 0);
            }
        }
    }
}
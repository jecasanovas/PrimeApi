using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using BLL.SearchParams;
using BLL.Specification;
using Core.Entities;

namespace BLL.Parameters
{
    public class UserParam : BaseSpecification<UserInfo>
    {
        public UserParam(SearchParamUsers searchParams, bool onlyCount = false)
        {

            this.PageIndex = searchParams.page;
            this.PageSize = searchParams.pageSize;
            this.OnlyCount = onlyCount;


            var OrderAsc = new List<Expression<Func<UserInfo, object>>>();
            var OrderDesc = new List<Expression<Func<UserInfo, object>>>();


            if (searchParams.Order != null && !string.IsNullOrEmpty(searchParams.Order.Trim()))
            {

                var splitorder = searchParams.Order.Split(',');
                var order = 0;
                splitorder.ToList().ForEach(x =>
                {

                    if (!String.IsNullOrEmpty(x) && x.Contains(char.Parse("A")))
                        if (order == 0) OrderAsc.Add(x => x.Email);
                        else OrderAsc.Add(x => x.Email);
                    else if (!String.IsNullOrEmpty(searchParams.Order) && x.Contains(char.Parse("D")))
                        if (order == 0) OrderDesc.Add(x => x.Surname);
                        else OrderDesc.Add(x => x.Surname);
                    order++;
                });
            }

            if (OrderAsc.Count == 0 && OrderDesc.Count == 0)
            {
                this.AddOrderBy(x => x.Email);
            }
            else
            {
                if (OrderAsc.Count > 0) this.FieldsAddOrderBy(OrderAsc.ToArray());
                if (OrderDesc.Count > 0) this.FieldsAddOrderByDesc(OrderDesc.ToArray());
            }


            Expression<Func<UserInfo, bool>> criteria = x =>
                (String.IsNullOrEmpty(searchParams.Email) || x.Email.ToLower().Contains(searchParams.Email.ToLower())) &&
                (String.IsNullOrEmpty(searchParams.Name) || x.Surname.ToLower().Contains(searchParams.Name.ToLower())) &&
                (!searchParams.id.HasValue || x.Id == searchParams.id);


            AddCondition(criteria);

            ApplyPaging(PageSize * (PageIndex - 1), PageSize);


        }

    }
}

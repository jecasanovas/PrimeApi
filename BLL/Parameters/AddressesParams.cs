using BLL.Models;
using BLL.SearchParams;
using BLL.Specification;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL.Parameters
{
    public class AddressesParams : BaseSpecification<Adresses>
    {
        public AddressesParams(SearchParamsAddresses searchParams, bool onlyCount = false)
        {
            this.PageIndex = searchParams.page;
            this.PageSize = searchParams.pageSize;
            this.OnlyCount = onlyCount;
            AddInclude(c => c.Country);


            var OrderAsc = new List<Expression<Func<Adresses, object>>>();
            var OrderDesc = new List<Expression<Func<Adresses, object>>>();


            if (searchParams.order != null && !string.IsNullOrEmpty(searchParams.order.Trim()))
            {

                var splitorder = searchParams.order.Split(',');
                var order = 0;
                splitorder.ToList().ForEach(x =>
                {

                    if (!String.IsNullOrEmpty(x) && x.Contains(char.Parse("A")))
                        if (order == 0) OrderAsc.Add(x => x.CP);
                        else OrderAsc.Add(x => x.CP);
                    else if (!String.IsNullOrEmpty(searchParams.order) && x.Contains(char.Parse("D")))
                        if (order == 0) OrderDesc.Add(x => x.Direction);
                        else OrderDesc.Add(x => x.Direction);
                    order++;
                });
            }

            if (OrderAsc.Count == 0 && OrderDesc.Count == 0)
            {
                this.AddOrderBy(x => x.Direction);
            }
            else
            {
                if (OrderAsc.Count > 0) this.FieldsAddOrderBy(OrderAsc.ToArray());
                if (OrderDesc.Count > 0) this.FieldsAddOrderByDesc(OrderDesc.ToArray());
            }


            Expression<Func<Adresses, bool>> criteria = x =>
            (String.IsNullOrEmpty(searchParams.Street) || x.Direction.ToLower().Contains(searchParams.Street.ToLower())) &&
            (String.IsNullOrEmpty(searchParams.CP) || x.Direction.ToLower().Contains(searchParams.CP.ToLower())) &&
                (x.UserInfoId == searchParams.UserInfoId);

            AddCondition(criteria);

            ApplyPaging(PageSize * (PageIndex - 1), PageSize);




        }
    }
}

using BLL.Models;
using BLL.SearchParams;
using BLL.Specification;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Parameters
{
    public class PaymentInfoParam : BaseSpecification<PaymentInfo>
    {

        public PaymentInfoParam(SearchParamsPaymentInfo searchParams, bool onlyCount = false)
        {
            this.PageIndex = searchParams.page;
            this.PageSize = searchParams.pageSize;
            this.OnlyCount = onlyCount;



            var OrderAsc = new List<Expression<Func<PaymentInfo, object>>>();
            var OrderDesc = new List<Expression<Func<PaymentInfo, object>>>();


            if (searchParams.order != null && !string.IsNullOrEmpty(searchParams.order.Trim()))
            {

                var splitorder = searchParams.order.Split(',');
                var order = 0;
                splitorder.ToList().ForEach(x =>
                {

                    if (!String.IsNullOrEmpty(x) && x.Contains(char.Parse("A")))
                        if (order == 0) OrderAsc.Add(x => x.Name);
                        else OrderAsc.Add(x => x.Name);
                    else if (!String.IsNullOrEmpty(searchParams.order) && x.Contains(char.Parse("D")))
                        if (order == 0) OrderDesc.Add(x => x.CardHolderName);
                        else OrderDesc.Add(x => x.CardHolderName);
                    order++;
                });
            }

            if (OrderAsc.Count == 0 && OrderDesc.Count == 0)
            {
                this.AddOrderBy(x => x.Name);
            }
            else
            {
                if (OrderAsc.Count > 0) this.FieldsAddOrderBy(OrderAsc.ToArray());
                if (OrderDesc.Count > 0) this.FieldsAddOrderByDesc(OrderDesc.ToArray());
            }


            Expression<Func<PaymentInfo, bool>> criteria = x =>
            (String.IsNullOrEmpty(searchParams.Name) || x.Name.ToLower().Contains(searchParams.Name.ToLower())) &&
            (String.IsNullOrEmpty(searchParams.CardHolder) || x.CardHolderName.Contains(searchParams.CardHolder.ToLower())) &&
                (x.UserInfoId == searchParams.UserInfoId);

            AddCondition(criteria);

            ApplyPaging(PageSize * (PageIndex - 1), PageSize);




        }
    }
}

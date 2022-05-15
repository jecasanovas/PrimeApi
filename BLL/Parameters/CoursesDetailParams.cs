using BLL.Models;
using BLL.Specification;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL.Parameters
{
    public class CourseDetailsParam : BaseSpecification<CourseDetail>
    {
        public CourseDetailsParam(SearchParamCourses searchParams, bool onlyCount = false)
        {

            this.PageIndex = searchParams.page;
            this.PageSize = searchParams.pageSize;
            this.OnlyCount = onlyCount;


            var OrderAsc = new List<Expression<Func<CourseDetail, dynamic>>>();
            var OrderDesc = new List<Expression<Func<CourseDetail, dynamic>>>();


            if (searchParams.order != null && !string.IsNullOrEmpty(searchParams.order.Trim()))
            {

                var splitorder = searchParams.order.Split(',');
                var order = 0;
                splitorder.ToList().ForEach(x =>
                {

                    if (!String.IsNullOrEmpty(x) && x.Contains(char.Parse("A")))
                        if (order == 0) OrderAsc.Add(x=> x.Lessonid);
                        else OrderAsc.Add(x => x.Description);
                    else if (!String.IsNullOrEmpty(searchParams.order) && x.Contains(char.Parse("D")))
                        if (order == 0) OrderDesc.Add(x => x.Lessonid);
                        else OrderDesc.Add(x =>x.Description);
                    order++;
                });
            }

            if (OrderAsc.Count == 0 && OrderDesc.Count == 0)
            {
                this.AddOrderBy(x => x.Lessonid);
            }
            else
            {
                if (OrderAsc.Count > 0) this.FieldsAddOrderBy(OrderAsc.ToArray());
                if (OrderDesc.Count > 0) this.FieldsAddOrderByDesc(OrderDesc.ToArray());
            }


            if (searchParams.CourseId > 0)
            {
                Expression<Func<CourseDetail, bool>> condition =
                    x => x.Courseid == searchParams.CourseId && (!searchParams.lessonID.HasValue || searchParams.lessonID == x.Lessonid )
                    && (String.IsNullOrEmpty(searchParams.description) || x.Description.ToLower().Trim().Contains(searchParams.description.ToLower()));

                ApplyPaging(PageSize * (PageIndex - 1), PageSize);


                AddOrderBy(x => x.Lessonid);
                /////** Add where **/
                this.AddCondition(condition);
            }

        }

    }
}

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
    public class CoursesParams : BaseSpecification<Course>
    {
        public CoursesParams(SearchParamCourses searchParams, bool onlyCount = false)
        {

            /*********************** Configuration Object **************************/


            this.PageIndex = searchParams.page;
            this.PageSize = searchParams.pageSize;
            this.OnlyCount = onlyCount;


            //** FROM **//
            AddInclude(c => c.Teacher);
            AddInclude(c => c.Teacher.Country);
            AddInclude(c => c.Technology);
            AddInclude(c => c.TechnologyDetails);




            //Order , multiorde
            var OrderAsc = new List<Expression<Func<Course, object>>>();
            var OrderDesc = new List<Expression<Func<Course, object>>>();


            if (searchParams.order != null && !string.IsNullOrEmpty(searchParams.order.Trim()))
            {

                var splitorder = searchParams.order.Split(',');
                var order = 0;
                splitorder.ToList().ForEach(x =>
                {

                    if (!String.IsNullOrEmpty(x) && x.Contains(char.Parse("A")))
                        if (order == 0) OrderAsc.Add(x => x.Name);
                        else OrderAsc.Add(x => x.Teacher.Name);
                    else if (!String.IsNullOrEmpty(searchParams.order) && x.Contains(char.Parse("D")))
                        if (order == 0) OrderDesc.Add(x => x.Name);
                        else OrderDesc.Add(x => x.Teacher.Name);
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


            List<int> idTeachers = new List<int>();

            if (!String.IsNullOrEmpty(searchParams.idteacher) && searchParams.idteacher != "0")
            {
                searchParams.idteacher.Split(',').ToList().ForEach(x =>
                                 idTeachers.Add(Int32.Parse(x)));
            }

            List<int> idtech = new List<int>();

            if (!String.IsNullOrEmpty(searchParams.idtec) && searchParams.idtec != "0")
            {
                searchParams.idtec.Split(',').ToList().ForEach(x =>
                                 idtech.Add(Int32.Parse(x)));
            }


            List<int> idtechdet = new List<int>();

            if (!String.IsNullOrEmpty(searchParams.idtechdet) && searchParams.idtechdet != "0")
            {
                searchParams.idtechdet.Split(',').ToList().ForEach(x =>
                                 idtechdet.Add(Int32.Parse(x)));
            }



            Expression<Func<Course, bool>> criteria = x =>
            ((String.IsNullOrEmpty(searchParams.name) || x.Name.ToLower().Contains(searchParams.name.ToLower()))) &&
                (idTeachers.Count == 0 || idTeachers.Contains(x.Teacher.Id)) &&
                (idtech.Count == 0 || idtech.Contains(x.Technology.Id)) &&
                (idtechdet.Count == 0 || idtechdet.Contains(x.TechnologyDetails.Id)) &&
                (!searchParams.id.HasValue || x.Id == searchParams.id);

            AddCondition(criteria);

            ApplyPaging(PageSize * (PageIndex - 1), PageSize);


        }
    }
}

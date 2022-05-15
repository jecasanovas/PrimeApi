using BLL.Models;
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
    public  class TeacherParams : BaseSpecification<Teacher>
    {
        public  TeacherParams(SearchParamTeachers searchParams, bool onlyCount = false)
        {

            this.PageIndex = searchParams.page;
            this.PageSize = searchParams.pageSize;
            this.OnlyCount = onlyCount;

           
            AddInclude(c => c.Country);
           

            //** Remove paging and order for count action
            
            
             ApplyPaging(PageSize * (PageIndex - 1), PageSize);
            
            
             AddOrderBy(x => x.Name);

            //** ADD SELECT Fields **/
            // AddReturningFields(returingFields);


        }

    }
}

using BLL.Models;
using BLL.Specification;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL.Parameters
{
    public class GenericParams<T> : BaseSpecification<T>
    {
        public GenericParams(SearchParam searchParams, bool onlyCount = false)
        {

            this.PageIndex = searchParams.page;
            this.PageSize = searchParams.pageSize;
            this.OnlyCount = onlyCount;

 
        }

    }
}

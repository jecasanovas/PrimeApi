using BLL.SearchParams;
using BLL.Specification;


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

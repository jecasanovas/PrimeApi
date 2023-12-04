using BLL.SearchParams;
using Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITeacherService
    {
        Task<int> InsertTeacherAsync(Teacher teacher, CancellationToken cancellationToken);
        Task<int> UpdateTeacherAsync(Teacher teacher, CancellationToken cancellationToken);
        Task<int> GetTotalRowsAsync(SearchParamTeachers searchParams, CancellationToken cancellationToken);
        Task<int> PostFileAsync(int id, Microsoft.AspNetCore.Http.IFormFile file, CancellationToken cancellationToken);
        Task<bool> DeleteTeacherAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Teacher>> GetTeachersAsync(SearchParamTeachers searchParameters, CancellationToken cancellationToken);
    }
}

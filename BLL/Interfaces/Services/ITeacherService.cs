using BLL.Dtos;
using BLL.Models;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITeacherService
    {

        Task<Teacher> InsertTeacher(Teacher teacher);
        Task<Teacher> UpdateTeacher(Teacher teacher);
        Task<int> GetTotalRowsAsysnc(SearchParamTeachers searchParams);
        Task<Teacher> PostFile(int id, Microsoft.AspNetCore.Http.IFormFile file);
        Task<int> DeleteTeacher(int id);
        Task<IEnumerable<Teacher>> GetTeachers(SearchParamTeachers searchParameters);
        
    }
}

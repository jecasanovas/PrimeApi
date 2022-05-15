using BLL.Dtos;
using BLL.Models;
using Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Repositories
{
    public interface ITeacherRepository
      {

        Task<Teacher> InsertTeacher(TeacherDto teacher);
        Task<Teacher> UpdateTeacher(TeacherDto teacher);

        Task DeleteTeacher(int id);


        Task<IEnumerable<Teacher>> GetTeachers(SearchParamTeachers searchTeacherParams);
        Task<int> GetTotalTeachers(SearchParamTeachers searchParameters);
        
         Task<Teacher> PostFile(int id, Microsoft.AspNetCore.Http.IFormFile file);
      
    }
}

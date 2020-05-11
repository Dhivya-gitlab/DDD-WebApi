using AspNetCore.ServiceRegistration.Dynamic.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Services
{
   public interface ISchoolService: IScopedService
    {
        Task<List<SchoolDetailsViewModel>> GetSchoolListAsync();

        Task CreateSchoolAsync(CreateSchoolViewModel createDepartmentViewModel);

        Task<SchoolDetailsViewModel> GetSchoolAsync(int departmentId);

        Task UpdateSchoolAsync(UpdateSchoolViewModel updateDepartmentViewModel);

        Task DeleteSchoolAsync(int departmentId);
    }
}

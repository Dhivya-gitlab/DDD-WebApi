using StudentEducationBoardService.Services.Dtos.SchoolDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StudentEducationBoardService.Services.ServicesInterface
{
    public interface ISchoolService
    {
        Task<List<SchoolDetailsDto>> GetSchoolList();

        void CreateSchool(CreateSchoolDto createSchoolDto);

        SchoolDetailsDto GetSchool(int schoolID);

        void UpdateSchool(int id, UpdateSchoolDto updateSchoolDto);

        void DeleteSchool(int schoolId);

    }
}

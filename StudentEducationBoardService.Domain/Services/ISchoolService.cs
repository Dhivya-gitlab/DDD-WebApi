using StudentEducationBoardService.Domain.SchoolDto.Dtos.SchoolDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentEducationBoardService.Domain.Services
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

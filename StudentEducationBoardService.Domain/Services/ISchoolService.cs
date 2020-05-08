using StudentEducationBoardService.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentEducationBoardService.Domain.Services
{
    public interface ISchoolService
    {
        Task<List<School>> GetSchoolList();

        void CreateSchool(School createSchool);

        School GetSchool(int schoolID);

        void UpdateSchool(int id, School updateSchool);

        void DeleteSchool(int schoolId);

    }
}

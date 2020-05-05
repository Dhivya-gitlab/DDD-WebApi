using StudentEducationBoardService.Domain.Models;
using System.Collections.Generic;

namespace StudentEducationBoardService.Domain.Repositories
{
    public interface ISchoolRepository : IRepository<School>
    {        IEnumerable<School> GetSchools(int schoolsCount);
    }
}

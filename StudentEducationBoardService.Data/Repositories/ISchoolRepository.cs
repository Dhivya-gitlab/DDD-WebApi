using StudentEducationBoardService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentEducationBoardService.Data.Repositories
{
  public  interface ISchoolRepository: IRepository<School>
    {        
        IEnumerable<School> GetSchools(int schoolsCount);
    }
}

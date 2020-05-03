using StudentEducationBoardService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentEducationBoardService.Data.Repositories
{
    public class SchoolRepository : Repository<School>, ISchoolRepository
    {
        private readonly StudentEducationBoardDbContext _schoolEntity;

        public SchoolRepository(StudentEducationBoardDbContext entityContext) : base(entityContext)
        {
            _schoolEntity = entityContext;
        }
        public IEnumerable<School> GetSchools(int schoolsCount)
        {
            throw new NotImplementedException();
        }
    }
}

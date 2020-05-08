using StudentEducationBoardService.Domain.Models;
using StudentEducationBoardService.Domain.Repositories;
using System;
using System.Collections.Generic;

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

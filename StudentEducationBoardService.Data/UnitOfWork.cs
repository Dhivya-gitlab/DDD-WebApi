using StudentEducationBoardService.Data.Repositories;
using StudentEducationBoardService.Domain;
using StudentEducationBoardService.Domain.Repositories;

namespace StudentEducationBoardService.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentEducationBoardDbContext _context;

        public ISchoolRepository Repository { get; set; }
        public UnitOfWork(StudentEducationBoardDbContext context)
        {
            _context = context;
            Repository = new SchoolRepository(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}

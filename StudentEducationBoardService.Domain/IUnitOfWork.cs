using StudentEducationBoardService.Domain.Repositories;
using System;

namespace StudentEducationBoardService.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        public ISchoolRepository Repository { get; set; }
        int Complete();
    }
}

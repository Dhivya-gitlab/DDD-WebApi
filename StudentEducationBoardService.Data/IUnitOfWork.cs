using StudentEducationBoardService.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentEducationBoardService.Data
{
    public interface IUnitOfWork : IDisposable
    {
        public ISchoolRepository Repository { get; }
        int Complete();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StudentEducationBoardService.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        Task<IQueryable<T>> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);

        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        void Update(T entity);
    }
}

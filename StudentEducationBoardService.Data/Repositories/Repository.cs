using Microsoft.EntityFrameworkCore;
using StudentEducationBoardService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StudentEducationBoardService.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly DbSet<T> _entities;

        public Repository(DbContext context)
        {
            _entities = context.Set<T>();
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public async Task<IQueryable<T>> GetAll()
        {
            var allEntities = await _entities.ToListAsync();
            return allEntities.AsQueryable();
        }

        public T GetById(int id)
        {
            return  _entities.Find(id);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _entities.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }
    }
}

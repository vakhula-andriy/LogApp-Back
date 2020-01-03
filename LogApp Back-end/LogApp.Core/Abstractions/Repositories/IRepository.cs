using System.Collections.Generic;
using System.Linq;
using LogApp.Core.Models;

namespace LogApp.Core.Abstractions.Repositories
{
    public interface IRepository<T> where T : IEntity<long>
    {
        public IQueryable<T> GetAll();
        public IQueryable<T> GetRange(int page, int pageSize = 25);
        public T GetById(long id);
        public T Add(T entity);
        public IQueryable<T> AddRange(List<T> entities);
        public void Edit(T entity);
        public void Delete(int id);
    }
}

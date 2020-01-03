using System.Collections.Generic;

namespace LogApp.Core.Abstractions.Services
{
    public interface IService<T> where T : class
    {
        public List<T> GetAll();
        public T GetById(int id);
        public T Insert(T entity);
        public List<T> InsertRange(List<T> entities);
        public T Update(T entity);
        public void Delete(int id);
    }
}

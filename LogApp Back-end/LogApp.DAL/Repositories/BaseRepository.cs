using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LogApp.Core.Models;
using LogApp.Core.Abstractions.Repositories;
using System.Collections.Generic;

namespace LogApp.DAL.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity<long>
    {
        protected readonly DbContext _context;
        public BaseRepository(DbContext context)
        {
            _context = context;
        }
        public virtual TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        public IQueryable<TEntity> AddRange(List<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            return entities.AsQueryable();
        }

        public void Delete(int id)
        {
            var entityToDel = GetById(id);
            _context.Set<TEntity>().Remove(entityToDel);
        }

        public void Edit(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public TEntity GetById(long id)
        {
            var res = _context.Set<TEntity>().Find(id);
            if (res != null && res.ID != 0)
                return res;
            throw new Exception("Invalid ID");
        }

        public virtual IQueryable<TEntity> GetRange(int page, int pageSize = 25)
        {
            return _context.Set<TEntity>().Skip(page * pageSize).Take(pageSize).AsQueryable();
        }
    }
}

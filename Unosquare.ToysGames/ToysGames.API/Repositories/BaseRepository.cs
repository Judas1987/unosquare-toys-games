using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ToysGames.API.Interfaces;

namespace ToysGames.API.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        public void Delete(T entityToDelete)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
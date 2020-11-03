using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToysGames.API.Interfaces;
using ToysGames.Data;

namespace ToysGames.API.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        #region Private members

        private ProductContext _productContext;
        private DbSet<T> _dbSet;

        #endregion

        public BaseRepository(ProductContext productContext)
        {
            _productContext = productContext;
            _dbSet = _productContext.Set<T>();
        }

        /// <summary>
        /// This method deletes an entity.
        /// </summary>
        /// <param name="entityToDelete">Represents the entity to be deleted</param>
        public void Delete(T entityToDelete)
        {
            if (_productContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// This method deletes an entity by its identifier.
        /// </summary>
        /// <param name="id">Represents the identifier of the entity to be deleted.</param>
        public void Delete(object id)
        {
            T entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// This method allows to query for specific data from the database.
        /// </summary>
        /// <param name="filter">Represents the filter to be executed against the database.</param>
        /// <param name="orderBy">Represents the ordering function.</param>
        /// <param name="includeProperties">Represents the property list to be included in the result.</param>
        /// <returns>An instance of <see cref="IEnumerable{T}"/></returns>
        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                    (new[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        /// <summary>
        /// This method gets an entity by its identifier.
        /// </summary>
        /// <param name="id">Represents the identifier of the entity to look for.</param>
        /// <returns>An instance of <see cref="T"/></returns>
        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// This method inserts a new entity into the data storage.
        /// </summary>
        /// <param name="entity">Represents the entity to be inserted.</param>
        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// This method updates an existing entity.
        /// </summary>
        /// <param name="entityToUpdate">Represents the entity to be update.</param>
        public void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _productContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ToysGames.API.Interfaces
{
    /// <summary>
    /// This interface is the signature required to implement the repository pattern described by Martin Fowler
    /// on his book Patterns of Enterprise Application Architecture <see href="https://www.amazon.com/Patterns-Enterprise-Application-Architecture-Martin/dp/0321127420/"> HERE </see>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// This method deletes an entity.
        /// </summary>
        /// <param name="entityToDelete">Represents the entity to be deleted</param>
        void Delete(T entityToDelete);

        /// <summary>
        /// This method deletes an entity by its identifier.
        /// </summary>
        /// <param name="id">Represents the identifier of the entity to be deleted.</param>
        void Delete(object id);

        /// <summary>
        /// This method allows to query for specific data from the database.
        /// </summary>
        /// <param name="filter">Represents the filter to be executed against the database.</param>
        /// <param name="orderBy">Represents the ordering function.</param>
        /// <param name="includeProperties">Represents the property list to be included in the result.</param>
        /// <returns>An instance of <see cref="IEnumerable{T}"/></returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");

        /// <summary>
        /// This method gets an entity by its identifier.
        /// </summary>
        /// <param name="id">Represents the identifier of the entity to look for.</param>
        /// <returns>An instance of <see cref="T"/></returns>
        T GetById(object id);

        /// <summary>
        /// This method inserts a new entity into the data storage.
        /// </summary>
        /// <param name="entity">Represents the entity to be inserted.</param>
        void Insert(T entity);

        /// <summary>
        /// This method updates an existing entity.
        /// </summary>
        /// <param name="entityToUpdate">Represents the entity to be update.</param>
        void Update(T entityToUpdate);
    }
}
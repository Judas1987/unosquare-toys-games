using ToysGames.Data.Models;

namespace ToysGames.API.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Represents the products repository.
        /// </summary>
        IRepository<Product> Products { get; }

        /// <summary>
        /// Executes the commit to the pending transactions.
        /// </summary>
        void Commit();
    }
}
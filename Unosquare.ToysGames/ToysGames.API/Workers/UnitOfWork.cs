using ToysGames.API.Interfaces;
using ToysGames.API.Repositories;
using ToysGames.Data;
using ToysGames.Data.Models;

namespace ToysGames.API.Workers
{
    /// <summary>
    /// Repositories manager.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Private members

        private IRepository<Product> _productsRepository;
        private ProductContext _context;

        #endregion

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="context">Represents the database context.</param>
        public UnitOfWork(ProductContext context)
        {
            _context = context;
            _productsRepository = new BaseRepository<Product>(_context);
        }

        /// <summary>
        /// Represents the products repository instance.
        /// </summary>
        public IRepository<Product> Products => _productsRepository ?? new BaseRepository<Product>(_context);

        /// <summary>
        /// This method commits the pending changes in the database.
        /// </summary>
        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
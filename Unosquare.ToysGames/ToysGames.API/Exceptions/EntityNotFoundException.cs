using System;

namespace ToysGames.API.Exceptions
{
    /// <summary>
    /// This exception can be thrown when a give entity is not found in the data source.
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}
using System;

namespace ToysGames.API.Exceptions
{
    /// <summary>
    /// This exception can be thrown when an incorrect request is passed.
    /// </summary>
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }
    }
}
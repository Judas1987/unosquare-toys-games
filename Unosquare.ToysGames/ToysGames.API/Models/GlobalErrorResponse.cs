using System;

namespace ToysGames.API.Models
{
    public class GlobalErrorResponse
    {
        /// <summary>
        /// Global error response constructor.
        /// </summary>
        /// <param name="ex">Represents the exception instance.</param>
        public GlobalErrorResponse(Exception ex)
        {
            Type = ex.GetType().Name;
            Message = ex.Message;
            StackTrace = ex.ToString();
        }
        
        /// <summary>
        /// Represents the error type.
        /// </summary>
        public string Type { get; }
        
        /// <summary>
        /// Represents the error message.
        /// </summary>
        public string Message { get;}
        
        /// <summary>
        /// Represents the error stack trace.
        /// </summary>
        public string StackTrace { get; }
    }
}
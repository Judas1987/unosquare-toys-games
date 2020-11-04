using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToysGames.API.Exceptions;
using ToysGames.API.Models;

namespace ToysGames.API.Controllers
{
    public class ErrorsController : ControllerBase
    {
        /// <summary>
        /// This method handles the requests sent to the error resource.
        /// </summary>
        /// <returns>A response.</returns>
        [Route("error")]
        public Task<GlobalErrorResponse> Error()
        {
            return Task.Factory.StartNew(() =>
            {
                IExceptionHandlerFeature context = HttpContext.Features.Get<IExceptionHandlerFeature>();

                var exception = context?.Error;
                var code = 500;

                if (exception is EntityNotFoundException) code = 404;
                else if (exception is BadRequestException) code = 400;

                Response.StatusCode = code;

                return new GlobalErrorResponse(exception);
            });
        }
    }
}
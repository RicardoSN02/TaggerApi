
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaggerApi.Services.ErrorServices;

namespace TaggerApi.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        public ErrorController()
        {
        }

        [Route("/error-development")]
        public IActionResult HandleErrorDevelopment(
        [FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature =
                HttpContext.Features.Get<IExceptionHandlerFeature>()!;
                var exception = exceptionHandlerFeature.Error;    

            if(exception is NotFoundException){
                return Problem(title: exceptionHandlerFeature.Error.Message,
                               detail: exceptionHandlerFeature.Error.StackTrace,
                               statusCode: 404);
            }         
            
            if(exception is UnauthorizedAccessException){
                return Problem(title: exceptionHandlerFeature.Error.Message,
                               detail: exceptionHandlerFeature.Error.StackTrace,
                               statusCode: 403);
            }               

            if(exception is ConflictException){
                return Problem(title: exceptionHandlerFeature.Error.Message,
                               detail: exceptionHandlerFeature.Error.StackTrace,
                               statusCode: 500);
            }   
            
            if(exception is InvalidOperationException){
                return Problem(title: exceptionHandlerFeature.Error.Message,
                                detail: exceptionHandlerFeature.Error.StackTrace,
                                statusCode: 500);
            }      

            if(exception is ArgumentException){
                return Problem(title: exceptionHandlerFeature.Error.Message,
                                detail: exceptionHandlerFeature.Error.StackTrace,
                                statusCode: 400);
            } 

            if(exception is KeyNotFoundException){
                return Problem(title: exceptionHandlerFeature.Error.Message,
                                detail: exceptionHandlerFeature.Error.StackTrace,
                                statusCode: 404);
            }     

            if(exception is ValidationException){
                return Problem(title: exceptionHandlerFeature.Error.Message,
                                detail: exceptionHandlerFeature.Error.StackTrace,
                                statusCode: 400);
            }

            if(exception is TimeoutException){
                return Problem(title: exceptionHandlerFeature.Error.Message,
                                detail: exceptionHandlerFeature.Error.StackTrace,
                                statusCode: 504);
            }             

            return Problem(title: "An unexpected error occurred",
                                detail: exceptionHandlerFeature.Error.StackTrace,
                                statusCode: 500);
        }

        [Route("/error")]
        public IActionResult HandleError()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
            var exception = exceptionHandlerFeature.Error;    

            if(exception is NotFoundException){
                return Problem(title: exceptionHandlerFeature.Error.Message,
                               statusCode: 404);
            }         
            
            if(exception is UnauthorizedAccessException){
                return Problem(title: exceptionHandlerFeature.Error.Message,
                               statusCode: 403);
            }               

            if(exception is ConflictException){
                return Problem(title: exceptionHandlerFeature.Error.Message,
                               statusCode: 500);
            }   
            
            if(exception is InvalidOperationException){
                return Problem(title: exceptionHandlerFeature.Error.Message,
                                statusCode: 500);
            }      

            if(exception is ArgumentException){
                return Problem(title: exceptionHandlerFeature.Error.Message,
                                statusCode: 400);
            } 

            if(exception is KeyNotFoundException){
                return Problem(title: exceptionHandlerFeature.Error.Message,
                                statusCode: 404);
            }     

            if(exception is ValidationException){
                return Problem(title: exceptionHandlerFeature.Error.Message,
                                statusCode: 400);
            }

            if(exception is TimeoutException){
                return Problem(title: exceptionHandlerFeature.Error.Message,
                                statusCode: 504);
            }             

            return Problem(title: "An unexpected error occurred",
                                statusCode: 500); 
            
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace BuberDinner.Api.Fillters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var propblemDetails = new ProblemDetails()
        {
            Title = "An error occured while processing your request",
            Status = (int)HttpStatusCode.InternalServerError
        };

        context.Result = new ObjectResult(propblemDetails)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
        };
        context.ExceptionHandled = true;
    }
}

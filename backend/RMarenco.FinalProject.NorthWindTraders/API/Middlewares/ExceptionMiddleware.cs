using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using NorthWindTraders.Application.CustomExceptions;

namespace NorthWindTraders.Api.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, context);
            }
        }

        private static async Task HandleExceptionAsync(Exception ex, HttpContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var details = string.Empty;
            var errors = new List<string>();
            switch (ex)
            {
                case ConflictException:
                    statusCode = HttpStatusCode.Conflict;
                    break;
                case NoContentException:
                    statusCode = HttpStatusCode.NoContent;
                    break;
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case BadRequestException badRequestEx:
                    statusCode = HttpStatusCode.BadRequest;
                    errors = badRequestEx.Errors;
                    break;
                default:
                    details = ex.StackTrace ?? "";
                    break;
            }

            var response = new { message = ex.Message, statusCode, details, errors };
            
            context.Response.StatusCode = (int)statusCode;
            if (statusCode == HttpStatusCode.NoContent)
            {
                return;
            }

            var jsonResponse = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
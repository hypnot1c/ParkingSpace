using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Web.Api.Model;

namespace Extensions.AspNetCore
{
  public class APIResponseMiddleware
  {
    public APIResponseMiddleware(
        ILogger<APIResponseMiddleware> logger,
        RequestDelegate next
        )
    {
      this._logger = logger;
      this._next = next;
      this._path = new PathString[0];
    }

    public APIResponseMiddleware(
        ILogger<APIResponseMiddleware> logger,
        RequestDelegate next,
        PathString[] path
        ) : this(logger, next)
    {
      this._path = path;
    }

    public APIResponseMiddleware(
        ILogger<APIResponseMiddleware> logger,
        RequestDelegate next,
        PathString path
        ) : this(logger, next)
    {
      this._path = new PathString[] { path };
    }

    private readonly ILogger<APIResponseMiddleware> _logger;
    private readonly RequestDelegate _next;
    private readonly PathString[] _path;

    public async Task Invoke(HttpContext context)
    {
      if (this._path.Length > 0 && !this._path.Any(p => context.Request.Path.StartsWithSegments(p)))
      {
        await _next.Invoke(context);
        return;
      }

      var originalBodyStream = context.Response.Body;
      string body = null;
      using (var responseBody = new MemoryStream())
      {
        context.Response.Body = responseBody;
        try
        {
          await this._next.Invoke(context);

          responseBody.Position = 0;
          body = new StreamReader(responseBody).ReadToEnd();

          object data;
          try
          {
            data = JsonConvert.DeserializeObject(body);
          }
          catch
          {
            this._logger.LogWarning($"Response body is not an object! Response body: {body.Substring(0, Math.Min(body.Length, 250))}");
            data = body;
          }

          IApiResponse apiResponse = null;

          this._logger.LogDebug($"Response status code is {context.Response.StatusCode}");

          switch ((HttpStatusCode)context.Response.StatusCode)
          {
            case HttpStatusCode.OK:
            case HttpStatusCode.NoContent:
              context.Response.StatusCode = (int)HttpStatusCode.OK; // 204 change to 200 with body
              apiResponse = new OkApiResponse<object>(ApiResponse.StatusEnum.Ok)
              {
                Result = data
              };
              break;
            case HttpStatusCode.Created:
              apiResponse = new OkApiResponse<object>(ApiResponse.StatusEnum.Ok)
              {
                Result = data
              };
              break;
            case HttpStatusCode.BadRequest:
              apiResponse = new BadRequestApiResponse(ApiResponse.StatusEnum.ValidationError)
              {
                ValidationErrors = data
              };
              break;
            case HttpStatusCode.Unauthorized:
              apiResponse = new UnauthorizedApiResponse(ApiResponse.StatusEnum.Error, data);
              break;
            case HttpStatusCode.Forbidden:
              apiResponse = new ForbidApiResponse(ApiResponse.StatusEnum.Error, data);
              break;
            case HttpStatusCode.NotFound:
              apiResponse = new NotFoundApiResponse(ApiResponse.StatusEnum.NotFound)
              {
                Errors = data
              };
              break;
            case HttpStatusCode.Redirect:
            case HttpStatusCode.TemporaryRedirect:
              return;
            default:
              apiResponse = new UnexpectedErrorApiResponse(ApiResponse.StatusEnum.Error)
              {
                Errors = data
              };
              break;
          }

          apiResponse.RequestId = context.Items["CorrelationId"].ToString();
          context.Response.ContentType = "application/json";
          var json = JsonConvert.SerializeObject(apiResponse);
          var buffer = Encoding.UTF8.GetBytes(json);
          context.Response.ContentLength = buffer.Length;
          await originalBodyStream.WriteAsync(buffer, 0, buffer.Length);
        }
        catch (Exception ex)
        {
          this._logger.LogError(ex, $"An unhandled error occurred");
          await HandleExceptionAsync(context, ex, originalBodyStream);
        }
        finally
        {
          context.Response.Body = originalBodyStream;
        }
      }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception, Stream originalBodyStream)
    {
      var msg = exception.GetBaseException().Message;

      var apiResponse = new UnexpectedErrorApiResponse(ApiResponse.StatusEnum.Error)
      {
        RequestId = context.Items["CorrelationId"].ToString(),
        Errors = new List<ApiErrorItem>
        {
          new ApiErrorItem
          {
            Code = (int)HttpStatusCode.InternalServerError,
            Message = msg
          }
        }
      };

      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
      context.Response.ContentType = "application/json";

      var json = JsonConvert.SerializeObject(apiResponse);
      var buffer = Encoding.UTF8.GetBytes(json);
      return originalBodyStream.WriteAsync(buffer, 0, buffer.Length);
    }
  }

  public static class ApiResponseMiddlewareExtension
  {
    public static IApplicationBuilder UseAPIResponseWrapperMiddleware(this IApplicationBuilder builder, params PathString[] path)
    {
      return builder.UseMiddleware<APIResponseMiddleware>(path);
    }

    public static IApplicationBuilder UseAPIResponseWrapperMiddleware(this IApplicationBuilder builder, PathString path)
    {
      return builder.UseMiddleware<APIResponseMiddleware>(path);
    }

    public static IApplicationBuilder UseAPIResponseWrapperMiddleware(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<APIResponseMiddleware>();
    }
  }
}

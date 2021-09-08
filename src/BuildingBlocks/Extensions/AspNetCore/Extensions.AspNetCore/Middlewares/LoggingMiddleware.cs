using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Extensions.AspNetCore
{
  public class LoggingMiddleware
  {
    public LoggingMiddleware(
      ILogger<LoggingMiddleware> logger,
      RequestDelegate next,
      bool LogHttpRequestExecTime = false
      )
    {
      this._logHttpRequestExecTime = LogHttpRequestExecTime;
      this._logger = logger;
      this._next = next;
    }

    private readonly bool _logHttpRequestExecTime;
    private readonly ILogger<LoggingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public async Task Invoke(HttpContext context)
    {
      if (context.Request.Headers.Any(c => c.Key.ToLower() == "x-portal-correlation-id"))
      {
        context.Items["CorrelationId"] = context.Request.Headers["x-portal-correlation-id"].ToString();
      }
      else
      {
        context.Items["CorrelationId"] = Guid.NewGuid().ToString();
      }

      _logger.LogInformation("Request start {0} {1}", context.Request.Method, context.Request.Path);

      if (this._logHttpRequestExecTime)
      {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        await _next(context);

        stopwatch.Stop();

        var elapsed_time = stopwatch.ElapsedMilliseconds;
        this._logger.LogInformation(
          "Request method: {0}, path: {1}, elapsed time: {2}",
          context.Request.Method,
          context.Request.Path,
          elapsed_time
          );
      }
      else
      {
        await _next(context);
      }

      _logger.LogInformation("Request completed with status code: {0}", context.Response.StatusCode);
    }
  }
  public static class LoggingExtensions
  {
    public static IApplicationBuilder UseCorrelationIdLogging(this IApplicationBuilder builder)
    {
      return builder.UseCorrelationIdLogging(false);
    }

    public static IApplicationBuilder UseCorrelationIdLogging(this IApplicationBuilder builder, bool logRequestTime)
    {
      return builder.UseMiddleware<LoggingMiddleware>(logRequestTime);
    }
  }
}

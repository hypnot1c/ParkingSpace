using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Extensions.AspNetCore
{
  public class GlobalExceptionFilter : IAsyncExceptionFilter, IExceptionFilter
  {
    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
      this._logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger is required");
    }

    private readonly ILogger<GlobalExceptionFilter> _logger;

    public void OnException(ExceptionContext context)
    {
      this._logger.LogError(context.Exception, "Unhandled exception");
    }

    public Task OnExceptionAsync(ExceptionContext context)
    {
      this.OnException(context);
      return Task.CompletedTask;
    }
  }
}

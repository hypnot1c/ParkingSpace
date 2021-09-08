using System;
using Diagnostics.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Extensions.AspNetCore
{
  public class HttpContextCorrelationIdProvider : ICorrelationIdProvider
  {
    public HttpContextCorrelationIdProvider(
      IHttpContextAccessor httpContextAccessor
      )
    {
      if (httpContextAccessor == null)
      {
        throw new ArgumentNullException(nameof(httpContextAccessor), "Http context accessor is required");
      }

      this._httpContextAccessor = httpContextAccessor;
    }

    private readonly IHttpContextAccessor _httpContextAccessor;
    public Guid GetCorrelationId()
    {
      var _httpContext = this._httpContextAccessor.HttpContext;
      if (!Guid.TryParse(_httpContext?.Items["CorrelationId"].ToString(), out var correlationId))
      {
        correlationId = Guid.NewGuid();
      }
      return correlationId;
    }
  }
}

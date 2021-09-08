using System;

namespace Diagnostics.Abstractions
{
  public interface ICorrelationIdProvider
  {
    Guid GetCorrelationId();
  }
}

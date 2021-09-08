using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace PS.Web.Api.Versions.V1
{
  public abstract class BaseAuthorizationHandler<TRequirement> : AuthorizationHandler<TRequirement> where TRequirement : IAuthorizationRequirement
  {
    protected BaseAuthorizationHandler(
      ILogger<BaseAuthorizationHandler<TRequirement>> logger
      )
    {
      this.Logger = logger;
    }

    protected ILogger<BaseAuthorizationHandler<TRequirement>> Logger { get; }
  }
}

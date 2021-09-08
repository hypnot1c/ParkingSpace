using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Extensions.AspNetCore
{
  public class AuthorizeResourceFilter : IAsyncActionFilter
  {
    public AuthorizeResourceFilter(
      IAuthorizationService authorizationService,
      Type targetResourceType,
      Type requirementType,
      int failStatusCode,
      string errorMessage
      )
    {
      _authorizationService = authorizationService;
      _targetResourceType = targetResourceType;
      _requirementType = requirementType;
      _failStatusCode = failStatusCode;
      _errorMessage = errorMessage;
    }

    private readonly IAuthorizationService _authorizationService;
    private readonly Type _targetResourceType;
    private readonly Type _requirementType;
    private readonly int _failStatusCode;
    private readonly string _errorMessage;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      object resource = null;
      if (context.ActionArguments.Any())
      {
        resource = context.ActionArguments.First().Value;
      }

      var constructors = _requirementType.GetConstructors().Where(c => c.IsPublic);

      var hasEmptyConstructor = !constructors.Any() || constructors.Any(c => c.GetParameters().Length == 0);
      var hasOneArgumentConstructor = constructors.Any(c => c.GetParameters().Length == 1);

      object requirement;
      if (hasEmptyConstructor)
      {
        requirement = Activator.CreateInstance(_requirementType);
      }
      else if (hasOneArgumentConstructor)
      {
        requirement = Activator.CreateInstance(_requirementType, this._targetResourceType);
      }
      else
      {
        requirement = Activator.CreateInstance(_requirementType, this._targetResourceType, context.ActionArguments);
      }

      var authorizationResult = await _authorizationService.AuthorizeAsync(context.HttpContext.User, resource, requirement as IAuthorizationRequirement);

      if (!authorizationResult.Succeeded)
      {
        context.Result = new ContentResult()
        {
          StatusCode = _failStatusCode,
          Content = _errorMessage,
          ContentType = "text/plain"
        };
        return;
      }

      await next();
    }
  }
}

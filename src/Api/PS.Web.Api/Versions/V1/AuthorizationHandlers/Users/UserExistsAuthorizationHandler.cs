using System;
using System.Threading.Tasks;
using Extensions.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using PS.Data.Master.Model;
using PS.DataService;

namespace PS.Web.Api.Versions.V1
{
  public class UserExistsAuthorizationHandler : BaseAuthorizationHandler<UserExistsRequirement>
  {
    public UserExistsAuthorizationHandler(
      IDataService dataService,
      ILogger<UserExistsAuthorizationHandler> logger
      ) : base(logger)
    {
      this.DataService = dataService;
    }

    public IDataService DataService { get; }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserExistsRequirement requirement)
    {
      var userId = 0;
      switch (requirement.TargetResourceType)
      {
        case Type userType when userType == typeof(UserModel):
          {
            switch (context.Resource)
            {
              case int id:
                userId = id;
                break;
              default:
                this.Logger.LogInformation("Unknown resource parameter for type {0}", requirement.TargetResourceType.Name);
                return;
            }
          }
          break;
        default:
          this.Logger.LogInformation("Unknown target resource parameter for requirement", requirement.GetType().Name);
          return;
      }

      var userExists = await this.DataService.Users.IsExists(userId);

      if (!userExists)
      {
        this.Logger.LogWarning("User with id {0} doesn't exist", userId);
        context.Fail();
        return;
      }

      context.Succeed(requirement);
    }
  }

  public class UserExistsRequirement : ResourceTypeBasedRequirement, IAuthorizationRequirement
  {
    public UserExistsRequirement(Type targetResourceType)
      : base(targetResourceType)
    {
    }
  }
}

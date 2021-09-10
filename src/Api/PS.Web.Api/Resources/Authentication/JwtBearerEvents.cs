using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using PS.DataService;

namespace PS.Web.Api.Resources
{
  public static class JwtBearerEvents
  {
    public static async Task OnTokenValidated(TokenValidatedContext tokenValidatedContext)
    {
      var email = tokenValidatedContext.Principal.FindFirstValue("email");

      var serviceProvider = tokenValidatedContext.HttpContext.RequestServices;
      var dataService = serviceProvider.GetRequiredService<IDataService>();

      var userId = await dataService.Users.GetIdByEmail(email);

      var claims = new List<Claim>
      {
        new Claim("sub", $"{userId}")
      };

      var appIdentity = new ClaimsIdentity(claims);

      tokenValidatedContext.Principal.AddIdentity(appIdentity);
    }
  }
}

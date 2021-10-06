using Nelibur.ObjectMapper;
using PS.Model;
using PS.Web.Api.Model.Output.V1;

namespace ParkingSpace.Resources
{
  public class SessionService
  {
    public UserSessionModel User { get; private set; }

    internal void SetSessionUser(UserOutputModel user)
    {
      this.User = TinyMapper.Map<UserSessionModel>(user);
    }
  }
}

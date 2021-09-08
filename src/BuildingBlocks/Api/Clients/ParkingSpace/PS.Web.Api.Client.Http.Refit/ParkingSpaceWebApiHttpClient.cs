namespace PS.Web.Api.Client
{
  [DefaultFor(typeof(IParkingSpaceWebApiClient))]
  public class ParkingSpaceWebApiHttpClient : IParkingSpaceWebApiClient
  {
    public ParkingSpaceWebApiHttpClient(
      IUsersArea usersArea
      )
    {
      this.Users = usersArea;
    }
    public IUsersArea Users { get; }
  }
}

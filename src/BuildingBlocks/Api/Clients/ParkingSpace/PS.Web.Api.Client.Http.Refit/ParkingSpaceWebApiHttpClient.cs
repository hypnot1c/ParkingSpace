using PS.Web.Api.Client.V1;

namespace PS.Web.Api.Client
{
  [DefaultFor(typeof(IParkingSpaceWebApiClient))]
  public class ParkingSpaceWebApiHttpClient : IParkingSpaceWebApiClient
  {
    public ParkingSpaceWebApiHttpClient(
      IV1VersionArea v1VersionArea
      )
    {
      this.V1 = v1VersionArea;
    }
    public IV1VersionArea V1 { get; }
  }
}

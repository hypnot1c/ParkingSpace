namespace PS.Web.Api.Client
{
  [DefaultFor(typeof(IParkingSpaceWebApiClient))]
  public class ParkingSpaceWebApiHttpClient : IParkingSpaceWebApiClient
  {
    public ParkingSpaceWebApiHttpClient(
      IUsersArea usersArea,
      IParkingPlacesArea parkingPlacesArea
      )
    {
      this.Users = usersArea;
      this.ParkingPlaces = parkingPlacesArea;
    }
    public IUsersArea Users { get; }
    public IParkingPlacesArea ParkingPlaces { get; }
  }
}

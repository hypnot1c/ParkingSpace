namespace PS.Web.Api.Client
{
  [DefaultFor(typeof(IParkingSpaceWebApiClient))]
  public class ParkingSpaceWebApiHttpClient : IParkingSpaceWebApiClient
  {
    public ParkingSpaceWebApiHttpClient(
      IUsersArea usersArea,
      IParkingPlacesArea parkingPlacesArea,
      IParkingGroupsArea parkingGroupsArea
      )
    {
      this.Users = usersArea;
      this.ParkingPlaces = parkingPlacesArea;
      this.ParkingGroups = parkingGroupsArea;
    }
    public IUsersArea Users { get; }
    public IParkingPlacesArea ParkingPlaces { get; }
    public IParkingGroupsArea ParkingGroups { get; }
  }
}

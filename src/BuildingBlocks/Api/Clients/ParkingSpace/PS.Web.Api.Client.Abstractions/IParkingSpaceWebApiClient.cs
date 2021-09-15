namespace PS.Web.Api.Client
{
  public interface IParkingSpaceWebApiClient
  {
    IUsersArea Users { get; }
    IParkingPlacesArea ParkingPlaces { get; }
    IParkingGroupsArea ParkingGroups { get; }
  }
}

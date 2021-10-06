using PS.Web.Api.Client.V1.Management;

namespace PS.Web.Api.Client.V1
{
  public interface IManagementArea
  {
    IParkingGroupsArea ParkingGroups { get; }
    IParkingPlacesArea ParkingPlaces { get; }
  }
}

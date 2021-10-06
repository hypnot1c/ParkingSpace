using PS.Web.Api.Client.V1.Management;

namespace PS.Web.Api.Client.V1
{
  [DefaultFor(typeof(IManagementArea))]
  public class ManagementArea : IManagementArea
  {
    public ManagementArea(
      IParkingGroupsArea parkingGroups,
      IParkingPlacesArea parkingPlaces
      )
    {
      this.ParkingGroups = parkingGroups;
      this.ParkingPlaces = parkingPlaces;
    }
    public IParkingGroupsArea ParkingGroups { get; }
    public IParkingPlacesArea ParkingPlaces { get; }
  }
}

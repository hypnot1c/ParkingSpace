namespace PS.Web.Api.Model.Output.V1.Management
{
  public class ParkingPlaceOutputModel
  {
    public ParkingPlaceOutputModel(ParkingGroupOutputModel group)
    {
      this.Group = group;
    }
    public ParkingPlaceOutputModel()
    {
      this.Group = new ParkingGroupOutputModel();
    }
    public int Id { get; set; }
    public ParkingGroupOutputModel Group { get; set; }
    public string Number { get; set; }
    public string Comment { get; set; }
  }
}

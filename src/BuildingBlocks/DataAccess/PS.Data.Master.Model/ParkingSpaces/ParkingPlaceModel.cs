namespace PS.Data.Master.Model
{
  public class ParkingPlaceModel : IdentityBaseModel
  {
    public int GroupId { get; set; }
    public ParkingGroupModel Group { get; set; }
    public string Number { get; set; }
    public string Comment { get; set; }
  }
}

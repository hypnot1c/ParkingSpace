namespace PS.Web.Api.Client
{
  public class ParkingSpaceWebApiOptionsBuilder
  {
    public ParkingSpaceWebApiOptionsBuilder()
    {
      this._opts = new ParkingSpaceWebApiOptions();
    }
    private readonly ParkingSpaceWebApiOptions _opts;

    public ParkingSpaceWebApiOptionsBuilder ConfigureBase(string url)
    {
      this._opts.BaseUrl = url;
      return this;
    }

    public ParkingSpaceWebApiOptions Build()
    {
      return this._opts;
    }
  }
}

using System;

namespace PS.Web.Api.Client
{
  public class ParkingSpaceWebApiClientException : Exception
  {
    public ParkingSpaceWebApiClientException(
      string message,
      Exception inner
      ) : base(message, inner)
    {

    }
  }
}

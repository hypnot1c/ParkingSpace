using System;
using System.Threading.Tasks;
using Authorization.Abstractions;

namespace ParkingSpace.Resources
{
  public class ParkingSpaceWebApiBearerTokenProvider : IBearerTokenProvider
  {
    public ParkingSpaceWebApiBearerTokenProvider(
      )
    {
    }

    public async Task<string> Get()
    {
      return String.Empty;
    }
  }
}

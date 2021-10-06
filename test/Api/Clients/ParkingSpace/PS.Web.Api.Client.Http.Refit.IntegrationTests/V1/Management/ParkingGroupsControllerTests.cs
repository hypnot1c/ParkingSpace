using System.Threading.Tasks;
using Xunit;

namespace PS.Web.Api.Client.Http.Refit.IntegrationTests.V1.Management
{
  public class ParkingGroupsControllerTests
  {
    public ParkingGroupsControllerTests(IParkingSpaceWebApiClient webApiClient)
    {
      this._webApiClient = webApiClient;
    }

    private readonly IParkingSpaceWebApiClient _webApiClient;

    [Fact]
    public async Task GetList_Default_NotEmpty()
    {
      var list = await this._webApiClient.V1.Management.ParkingGroups.Get();

      Assert.NotEmpty(list);
    }
  }
}

using System.Threading;
using System.Threading.Tasks;
using Api.Google.Client;
using MediatR;
using Nelibur.ObjectMapper;
using PS.Web.Api.Client.Abstractions;
using PS.Web.Api.Client.Model.Input;

namespace ParkingSpace.Mediator
{
  public class UserAuthenticatedNotificationHandler : INotificationHandler<UserAuthenticatedNotification>
  {
    public UserAuthenticatedNotificationHandler(
      IGoogleApiClient googleApiClient,
      IParkingSpaceWebApiClient psWebApiClient
      )
    {
      this._googleApiClient = googleApiClient;
      this._psWebApiClient = psWebApiClient;
    }

    private readonly IGoogleApiClient _googleApiClient;
    private readonly IParkingSpaceWebApiClient _psWebApiClient;

    public async Task Handle(UserAuthenticatedNotification notification, CancellationToken cancellationToken)
    {
      var googleUser = await this._googleApiClient.GetUserAsync();

      var userInputModel = TinyMapper.Map<UserInputModel>(googleUser);

      var user = await this._psWebApiClient.User_EnsureCreated(userInputModel);
    }
  }
}

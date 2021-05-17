using System.Threading;
using System.Threading.Tasks;
using Api.Google.Client;
using MediatR;
using Nelibur.ObjectMapper;
using ParkingSpace.Resources;
using PS.Web.Api.Client;
using PS.Web.Api.Client.Model.Input;

namespace ParkingSpace.Mediator
{
  public class UserAuthenticatedNotificationHandler : INotificationHandler<UserAuthenticatedNotification>
  {
    public UserAuthenticatedNotificationHandler(
      SessionService sessionService,
      IGoogleApiClient googleApiClient,
      IParkingSpaceWebApiClient psWebApiClient
      )
    {
      this._sessionService = sessionService;
      this._googleApiClient = googleApiClient;
      this._psWebApiClient = psWebApiClient;
    }

    private readonly SessionService _sessionService;
    private readonly IGoogleApiClient _googleApiClient;
    private readonly IParkingSpaceWebApiClient _psWebApiClient;

    public async Task Handle(UserAuthenticatedNotification notification, CancellationToken cancellationToken)
    {
      var googleUser = await this._googleApiClient.GetUserAsync();

      var userInputModel = TinyMapper.Map<UserInputModel>(googleUser);

      var user = await this._psWebApiClient.User_EnsureCreated(userInputModel);

      //this._sessionService.SetSessionUser(user);

    }
  }
}

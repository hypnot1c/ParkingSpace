using ParkingSpace.Resources;
using Prism.Mvvm;
using PS.Model;

namespace ParkingSpace.ViewModels
{
  public class CalendarViewModel : BindableBase
  {
    public CalendarViewModel(
      SessionService sessionService
      )
    {
      this._sessionService = sessionService;

      this.User = this._sessionService.User;
    }

    private readonly SessionService _sessionService;


    private UserSessionModel _user;
    public UserSessionModel User
    {
      get => _user;
      set => SetProperty(ref _user, value);
    }
  }
}

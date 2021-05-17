using Prism.Mvvm;

namespace ParkingSpace.ViewModels
{
  public class CalendarViewModel : BindableBase
  {
    public CalendarViewModel()
    {
    }
    private string _message;

    public string Message
    {
      get => _message;
      set => SetProperty(ref _message, value);
    }
  }
}

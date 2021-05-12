using Prism.Mvvm;

namespace ParkingSpace.ViewModels
{
  public class MainViewModel : BindableBase
  {
    public MainViewModel()
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

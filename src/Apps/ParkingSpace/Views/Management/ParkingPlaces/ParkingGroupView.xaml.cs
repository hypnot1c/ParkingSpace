
using System.Threading.Tasks;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParkingSpace.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ParkingGroupView : ContentPage, INavigationAware
  {
    public ParkingGroupView()
    {
      InitializeComponent();
    }

    public void OnNavigatedFrom(INavigationParameters parameters)
    {
      this.Navigation.RemovePage(this);
    }

    public async void OnNavigatedTo(INavigationParameters parameters)
    {
      await Task.CompletedTask;
    }
  }
}

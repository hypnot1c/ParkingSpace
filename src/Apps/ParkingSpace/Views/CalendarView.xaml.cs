using Microsoft.Extensions.Logging;
using Xamarin.Forms;

namespace ParkingSpace.Views
{
  public partial class CalendarView : ContentPage
  {
    public CalendarView(
      ILogger<CalendarView> logger
      )
    {
      InitializeComponent();

      this.Logger = logger;
    }

    public ILogger<CalendarView> Logger { get; }

    private void ToggleCalendarSectionVisibility(object sender, SwipedEventArgs e)
    {
      this.Logger.LogInformation("SWIPED!");
      calendar.CalendarSectionShown = true;
    }
  }
}

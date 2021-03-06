using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using ParkingSpace.Resources;

namespace ParkingSpace.Droid.Interceptoris
{
  [Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
  [IntentFilter(
  new[] { Intent.ActionView },
  Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
  DataSchemes = new[] { "com.googleusercontent.apps.51862517676-v7j9degm2ail3gldk889n798ng5cku3m" },
  DataPath = "/oauth2redirect")]
  public class CustomUrlSchemeInterceptorActivity : Activity
  {
    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);

      // Convert Android.Net.Url to Uri
      var uri = new Uri(Intent.Data.ToString());

      // Load redirectUrl page
      AuthenticationState.Authenticator.OnPageLoading(uri);

      var intent = new Intent(this, typeof(MainActivity));
      intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
      StartActivity(intent);

      this.Finish();

      return;
    }
  }
}

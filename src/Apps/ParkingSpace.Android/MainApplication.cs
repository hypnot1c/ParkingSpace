using Android.App;
using Android.Runtime;
using Shiny;
using System;

namespace ParkingSpace.Droid
{
  [Application(Label = "Prism Sample", Icon = "@mipmap/icon")]
  public class MainApplication : Application
  {
    public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
        : base(javaReference, transfer)
    {
    }

    public override void OnCreate()
    {
      base.OnCreate();
      this.ShinyOnCreate(new Startup());
    }
  }
}
using System;
using System.IO;
using ParkingSpace.Droid;
using ParkingSpace.Resources;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidDbPath))]
namespace ParkingSpace.Droid
{
  public class AndroidDbPath : IPath
  {
    public string GetDatabasePath(string filename)
    {
      return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);
    }
  }
}

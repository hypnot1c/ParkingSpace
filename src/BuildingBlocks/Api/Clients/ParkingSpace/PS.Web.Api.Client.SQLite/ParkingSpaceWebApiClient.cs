using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PS.Data.Master.Context;
using PS.Data.Master.Model;
using PS.Web.Api.Client.Model.Input;
using PS.Web.Api.Client.Model.Output;

namespace PS.Web.Api.Client
{
  public class ParkingSpaceWebApiClient : IParkingSpaceWebApiClient, IDisposable, IAsyncDisposable
  {
    public ParkingSpaceWebApiClient(string databasePath)
    {
      this._databasePath = databasePath;

      var optionsBuilder = new DbContextOptionsBuilder<MasterContext>();
      var options = optionsBuilder
        .UseSqlite($"Filename={this._databasePath}")
        .Options
        ;

      this._masterContext = new MasterContext(options);

      this._masterContext.Database.EnsureCreated();
    }

    private string _databasePath;
    private MasterContext _masterContext;

    public void Dispose()
    {
      this._masterContext.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
      await this._masterContext.DisposeAsync();
    }

    public async Task<UserOutputModel> User_EnsureCreated(UserInputModel user)
    {
      var dbo = new UserModel
      {
        Id = 1,
        Firstname = user.Firstname,
        Lastname = user.Lastname,
        Email = user.Email,
        AvatarUrl = user.AvatarUrl
      };

      this._masterContext.Users.Add(dbo);

      await this._masterContext.SaveChangesAsync();

      return await this.User_GetAsync(dbo.Email);
    }

    public async Task<UserOutputModel> User_GetAsync(string email)
    {
      var t = await this._masterContext.Users
        .Where(u => u.Email == email)
        .SingleOrDefaultAsync()
        ;

      var res = new UserOutputModel
      {
        Id = t.Id,
        Firstname = t.Firstname,
        Lastname = t.Lastname,
        Email = t.Email,
        AvatarUrl = t.AvatarUrl
      };

      return res;
    }
  }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PS.Data.Master.Context;

namespace PS.DataService
{
  public class UsersDataService : IUsersDataService
  {
    public UsersDataService(
      IMemoryCache memoryCache,
      MasterContext masterContext
      )
    {
      this._memoryCache = memoryCache;
      this._masterContext = masterContext;
    }

    private readonly IMemoryCache _memoryCache;
    private readonly MasterContext _masterContext;

    public async Task<bool> IsExists(int id)
    {
      var userExists = await this._memoryCache.GetOrCreateAsync<bool>($"users-{id}-exists", async entry =>
      {
        var result = await this._masterContext.Users
          .Where(u => u.Id == id)
          .AnyAsync()
          ;

        entry.SetSlidingExpiration(TimeSpan.FromMinutes(2));
        return result;
      });

      return userExists;
    }

    public async Task<int> GetIdByEmail(string email)
    {
      var userId = await this._memoryCache.GetOrCreateAsync<int>($"users-{email}-id", async entry =>
      {
        var result = await this._masterContext.Users
          .Where(u => u.Email.ToLower() == email.ToLower())
          .Select(u => u.Id)
          .SingleOrDefaultAsync()
          ;

        entry.SetSlidingExpiration(TimeSpan.FromHours(1));
        return result;
      });

      return userId;
    }
  }
}

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
  }
}

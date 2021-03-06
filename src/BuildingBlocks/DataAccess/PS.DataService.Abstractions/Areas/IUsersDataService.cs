using System.Threading.Tasks;

namespace PS.DataService
{
  public interface IUsersDataService
  {
    Task<bool> IsExists(int id);
    Task<int> GetIdByEmail(string email);
  }
}

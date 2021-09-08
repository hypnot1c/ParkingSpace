namespace PS.DataService
{
  public class DataService : IDataService
  {
    public DataService(
      IUsersDataService usersDataService
      )
    {
      this.Users = usersDataService;
    }

    public IUsersDataService Users { get; }
  }
}

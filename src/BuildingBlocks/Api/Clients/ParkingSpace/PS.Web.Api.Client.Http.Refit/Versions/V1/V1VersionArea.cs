namespace PS.Web.Api.Client.V1
{
  [DefaultFor(typeof(IV1VersionArea))]
  public class V1VersionArea : IV1VersionArea
  {
    public V1VersionArea(
      IManagementArea management,
      IUsersArea users
      )
    {
      this.Management = management;
      this.Users = users;
    }
    public IManagementArea Management { get; }

    public IUsersArea Users { get; }
  }
}

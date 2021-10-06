namespace PS.Web.Api.Client.V1
{
  public interface IV1VersionArea
  {
    IManagementArea Management { get; }
    IUsersArea Users { get; }
  }
}

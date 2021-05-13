namespace PS.Xamarin.Authentication
{
  public class AuthenticationServiceOptions
  {
    public AuthenticationServiceOptions()
    {

    }
    public AuthenticationServiceOptions(GoogleProviderSettings googleProviderSettings)
      : base()
    {
      this.GoogleProviderSettings = googleProviderSettings;
      this.SecureStorageKeys = new SecureStorageKeys();
    }
    public SecureStorageKeys SecureStorageKeys { get; set; }
    public GoogleProviderSettings GoogleProviderSettings { get; set; }
  }

  public class SecureStorageKeys
  {
    public string UserAccount { get; set; }
    public string UserTokenIssueDate { get; set; }
    public string UserRefreshToken { get; set; }
  }
}

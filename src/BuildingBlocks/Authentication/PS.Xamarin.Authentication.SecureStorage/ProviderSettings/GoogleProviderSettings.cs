namespace PS.Xamarin.Authentication
{
  public class GoogleProviderSettings
  {
    public string ClientId { get; set; }
    public string Scopes { get; set; }
    public string AuthorizeUrl { get; set; }
    public string RedirectUri { get; set; }
    public string AccessTokenUrl { get; set; }
    public string UserInfoUrl { get; set; }
  }
}

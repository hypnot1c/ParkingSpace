using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Web.Api.Client
{
  public class AccessTokenDelegatingHandler : DelegatingHandler
  {
    public AccessTokenDelegatingHandler(HttpClient httpClient, IAccessTokenProvider accessTokenProvider)
    {
      this.HttpClient = httpClient;
      this._accessTokenProvider = accessTokenProvider;
    }

    public HttpClient HttpClient { get; }

    private readonly IAccessTokenProvider _accessTokenProvider;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
      )
    {
      var accessToken = await this._accessTokenProvider.Get();

      request.SetBearerToken(accessToken);
      return await base.SendAsync(request, cancellationToken);
    }
  }
}

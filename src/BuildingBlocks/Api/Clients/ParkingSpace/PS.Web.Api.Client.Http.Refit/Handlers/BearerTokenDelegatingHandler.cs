using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Authorization.Abstractions;

namespace PS.Web.Api.Client
{
  public class BearerTokenDelegatingHandler : DelegatingHandler
  {
    public BearerTokenDelegatingHandler(HttpClient httpClient, IBearerTokenProvider accessTokenProvider)
    {
      this.HttpClient = httpClient;
      this._bearerTokenProvider = accessTokenProvider;
    }

    public HttpClient HttpClient { get; }

    private readonly IBearerTokenProvider _bearerTokenProvider;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
      )
    {
      var accessToken = await this._bearerTokenProvider.Get();

      request.SetBearerToken(accessToken);
      return await base.SendAsync(request, cancellationToken);
    }
  }
}

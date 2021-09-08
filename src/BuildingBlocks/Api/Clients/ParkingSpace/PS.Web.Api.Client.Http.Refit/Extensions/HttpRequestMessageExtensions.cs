using System.Net.Http;
using System.Net.Http.Headers;

namespace PS.Web.Api.Client
{
  public static class HttpRequestMessageExtensions
  {
    public static void SetBearerToken(this HttpRequestMessage httpRequestMessage, string token)
    {
      httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
  }
}

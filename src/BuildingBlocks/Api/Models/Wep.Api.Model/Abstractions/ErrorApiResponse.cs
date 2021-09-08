using Newtonsoft.Json;

namespace Web.Api.Model
{
  public class ErrorApiResponse : ApiResponse
  {
    public ErrorApiResponse(StatusEnum status) : base(status)
    {
    }

    [JsonProperty("errors", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public object Errors { get; set; }
  }
}

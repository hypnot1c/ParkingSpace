using Newtonsoft.Json;

namespace Web.Api.Model
{
  public class OkApiResponse<TResult> : ApiResponse
  {
    public OkApiResponse() : base()
    {

    }
    public OkApiResponse(StatusEnum status) : base(status)
    {
    }

    [JsonProperty("result", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public TResult Result { get; set; }
  }
}

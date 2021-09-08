using Newtonsoft.Json;

namespace Web.Api.Model
{
  public class BadRequestApiResponse : ErrorApiResponse
  {
    public BadRequestApiResponse(StatusEnum status) : base(status)
    {

    }

    [JsonProperty("validationErrors", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public object ValidationErrors { get; set; }
  }
}

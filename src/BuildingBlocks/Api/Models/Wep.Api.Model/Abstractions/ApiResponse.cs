using System;
using Newtonsoft.Json;

namespace Web.Api.Model
{
  public abstract class ApiResponse : IApiResponse
  {
    public ApiResponse()
    {

    }
    public ApiResponse(StatusEnum status)
    {
      Status = Enum.GetName(typeof(StatusEnum), status);
    }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("requestId")]
    public string RequestId { get; set; }

    public enum StatusEnum
    {
      Ok,
      Error,
      ValidationError,
      NotFound
    }
  }
}

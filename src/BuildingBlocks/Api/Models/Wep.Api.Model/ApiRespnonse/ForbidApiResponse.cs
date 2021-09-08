using System.Collections.Generic;
using System.Net;

namespace Web.Api.Model
{
  public class ForbidApiResponse : ErrorApiResponse
  {
    public ForbidApiResponse(StatusEnum status) : base(status)
    {
    }

    public ForbidApiResponse(StatusEnum status, object errors) : this(status)
    {
      this.Errors = errors ?? new List<ApiErrorItem>
      {
        new ApiErrorItem
        {
          Code = (int)HttpStatusCode.Forbidden,
          Message = "Not authorized!"
        }
      };
    }
  }
}

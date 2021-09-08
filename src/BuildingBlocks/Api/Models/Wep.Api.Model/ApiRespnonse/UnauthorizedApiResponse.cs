using System.Collections.Generic;
using System.Net;

namespace Web.Api.Model
{
  public class UnauthorizedApiResponse : ErrorApiResponse
  {
    public UnauthorizedApiResponse(StatusEnum status) : base(status)
    {

    }

    public UnauthorizedApiResponse(StatusEnum status, object errors) : this(status)
    {
      this.Errors = errors ?? new List<ApiErrorItem> { new ApiErrorItem { Code = (int)HttpStatusCode.Unauthorized, Message = "Not authorized!" } };
    }
  }
}

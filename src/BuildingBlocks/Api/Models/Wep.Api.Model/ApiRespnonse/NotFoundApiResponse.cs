namespace Web.Api.Model
{
  public class NotFoundApiResponse : ErrorApiResponse
  {
    public NotFoundApiResponse(StatusEnum status) : base(status)
    {
    }
  }
}

namespace Web.Api.Model
{
  public class UnexpectedErrorApiResponse : ErrorApiResponse
  {
    public UnexpectedErrorApiResponse(StatusEnum status) : base(status)
    {
    }
  }
}

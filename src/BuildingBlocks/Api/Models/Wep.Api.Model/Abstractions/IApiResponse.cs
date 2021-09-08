namespace Web.Api.Model
{
  public interface IApiResponse
  {
    string Status { get; set; }
    string RequestId { get; set; }
  }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PS.Web.Api.Abstractions
{
  /// <summary>
  /// 
  /// </summary>
  [ApiController]
  [Produces("application/json")]
  public abstract class ApiBaseController : ControllerBase
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    public ApiBaseController(
      ILogger<ApiBaseController> logger
      )
    {
      this.Logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    protected ILogger<ApiBaseController> Logger { get; }
  }
}

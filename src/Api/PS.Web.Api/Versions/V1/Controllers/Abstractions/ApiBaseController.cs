using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PS.Web.Api.Versions.V1
{
  /// <summary>
  /// 
  /// </summary>
  [Route("v1/[controller]")]
  public abstract class ApiBaseController : Abstractions.ApiBaseController
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    public ApiBaseController(
      IMapper mapper,
      IMediator mediator,
      ILogger<ApiBaseController> logger
      ) : base(logger)
    {
      this.Mapper = mapper;
      this.Mediator = mediator;
    }

    protected IMapper Mapper { get; }
    protected IMediator Mediator { get; }
  }
}

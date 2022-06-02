using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PS.Web.Api.Model.Input;
using PS.Web.Api.Model.Output.V1.Management;

namespace PS.Web.Api.Versions.V1.Management
{
  public class ParkingGroupsController : ApiBaseController
  {
    public ParkingGroupsController(
      IMapper mapper,
      IMediator mediator,
      ILogger<ParkingGroupsController> logger
      ) : base(mapper, mediator, logger)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ParkingGroupOutputModel>> Get(int id)
    {
      var pgListRequest = new ParkingGroupGetRequest(id);

      var result = await this.Mediator.Send(pgListRequest);

      return Ok(result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParkingGroupOutputModel>>> Get()
    {
      var pgListRequest = new ParkingGroupsGetRequest();

      var result = await this.Mediator.Send(pgListRequest);

      return Ok(result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="im"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ParkingGroupOutputModel>> Create(ParkingGroupInputModel im)
    {
      IRequest<ParkingGroupOutputModel> request = null;

      if(im.Id is null)
      {
        request = this.Mapper.Map<ParkingGroupCreateRequest>(im);
      }

      var result = await this.Mediator.Send(request);

      return Ok(result);
    }
  }
}

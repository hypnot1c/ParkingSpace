using System.Threading.Tasks;
using AutoMapper;
using Extensions.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PS.Data.Master.Model;
using PS.Web.Api.Model.Input;
using PS.Web.Api.Model.Output;

namespace PS.Web.Api.Versions.V1
{
  public class UsersController : ApiBaseController
  {
    public UsersController(
      IMapper mapper,
      IMediator mediator,
      ILogger<UsersController> logger
      ) : base(mapper, mediator, logger)
    {
      
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [AuthorizeResource(typeof(UserModel), typeof(UserExistsRequirement), StatusCodes.Status404NotFound, "User not found")]
    public async Task<ActionResult<UserOutputModel>> Get(int id)
    {
      var request = new UserGetRequest(id);
      var user = await this.Mediator.Send(request);

      return Ok(user);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="im"></param>
    /// <returns></returns>
    [HttpPost("ensure-created")]
    public async Task<ActionResult<UserOutputModel>> EnsureCreated(UserInputModel im)
    {
      var request = this.Mapper.Map<UserEnsureCreatedRequest>(im);
      var user = await this.Mediator.Send(request);

      return Ok(user);
    }
  }
}

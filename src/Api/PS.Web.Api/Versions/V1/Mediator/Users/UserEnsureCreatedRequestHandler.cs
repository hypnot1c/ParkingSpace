using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PS.Data.Master.Context;
using PS.Web.Api.Model.Output;

namespace PS.Web.Api.Versions.V1
{
  public class UserEnsureCreatedRequestHandler : BaseRequestHandler, IRequestHandler<UserEnsureCreatedRequest, UserOutputModel>
  {
    public UserEnsureCreatedRequestHandler(
      IMediator mediator,
      IMapper mapper,
      MasterContext masterContext
      ) : base(mapper, masterContext)
    {
      this._mediator = mediator;
    }

    private readonly IMediator _mediator;

    public async Task<UserOutputModel> Handle(UserEnsureCreatedRequest request, CancellationToken cancellationToken)
    {
      var user = await this.Mapper.ProjectTo<UserOutputModel>(
        this.MasterContext.Users
          .Where(u => u.Email.ToLower() == request.Email)
        )
        .SingleOrDefaultAsync()
        ;

      if(user != null)
      {
        return user;
      }

      var userCreateRequest = this.Mapper.Map<UserCreateRequest>(request);

      user = await this._mediator.Send(userCreateRequest);

      return user;
    }
  }
}

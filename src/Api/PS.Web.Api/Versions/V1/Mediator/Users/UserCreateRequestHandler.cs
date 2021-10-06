using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PS.Data.Master.Context;
using PS.Data.Master.Model;
using PS.Web.Api.Model.Output.V1;

namespace PS.Web.Api.Versions.V1
{
  public class UserCreateRequestHandler : BaseRequestHandler, IRequestHandler<UserCreateRequest, UserOutputModel>
  {
    public UserCreateRequestHandler(
      IMediator mediator,
      IMapper mapper,
      MasterContext masterContext
      ) : base(mapper, masterContext)
    {
      this._mediator = mediator;
    }

    private readonly IMediator _mediator;

    public async Task<UserOutputModel> Handle(UserCreateRequest request, CancellationToken cancellationToken)
    {
      var userDBO = this.Mapper.Map<UserModel>(request);

      this.MasterContext.Users.Add(userDBO);
      await this.MasterContext.SaveChangesAsync(cancellationToken);

      var userGetRequest = new UserGetRequest(userDBO.Id);
      var user = await this._mediator.Send(userGetRequest, cancellationToken);

      return user;
    }
  }
}

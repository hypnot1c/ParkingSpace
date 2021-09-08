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
  public class UserGetRequestHandler : BaseRequestHandler, IRequestHandler<UserGetRequest, UserOutputModel>
  {
    public UserGetRequestHandler(
      IMapper mapper,
      MasterContext masterContext
      ) : base(mapper, masterContext)
    {

    }

    public async Task<UserOutputModel> Handle(UserGetRequest request, CancellationToken cancellationToken)
    {
      var userId = request.Id;

      var user = await this.Mapper.ProjectTo<UserOutputModel>(
          this.MasterContext.Users.Where(u => u.Id == userId)
        )
        .SingleAsync(cancellationToken)
        ;

      return user;
    }
  }
}

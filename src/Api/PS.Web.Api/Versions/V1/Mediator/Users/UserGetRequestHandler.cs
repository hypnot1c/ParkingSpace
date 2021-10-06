using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PS.Data.Master.Context;
using PS.DataService;
using PS.Web.Api.Model.Output.V1;

namespace PS.Web.Api.Versions.V1
{
  public class UserGetRequestHandler : BaseRequestHandler, IRequestHandler<UserGetRequest, UserOutputModel>
  {
    public UserGetRequestHandler(
      IDataService dataService,
      IMapper mapper,
      MasterContext masterContext
      ) : base(mapper, masterContext)
    {
      this._dataService = dataService;
    }

    private readonly IDataService _dataService;

    public async Task<UserOutputModel> Handle(UserGetRequest request, CancellationToken cancellationToken)
    {
      int? userId = request.Id;

      if(userId is null)
      {
        userId = await this._dataService.Users.GetIdByEmail(request.Email);
      }

      var user = await this.Mapper.ProjectTo<UserOutputModel>(
          this.MasterContext.Users.Where(u => u.Id == userId)
        )
        .SingleAsync(cancellationToken)
        ;

      return user;
    }
  }
}

using AutoMapper;
using PS.Data.Master.Context;

namespace PS.Web.Api.Versions.V1.Management
{
  public abstract class BaseRequestHandler
  {
    public BaseRequestHandler(
      IMapper mapper,
      MasterContext masterContext
      )
    {
      this.Mapper = mapper;
      this.MasterContext = masterContext;
    }

    public IMapper Mapper { get; }
    public MasterContext MasterContext { get; }
  }
}

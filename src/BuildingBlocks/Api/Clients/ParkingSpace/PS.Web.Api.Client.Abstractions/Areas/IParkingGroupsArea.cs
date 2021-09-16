using System.Collections.Generic;
using System.Threading.Tasks;
using PS.Web.Api.Model.Input;
using PS.Web.Api.Model.Output;

namespace PS.Web.Api.Client
{
  public interface IParkingGroupsArea
  {
    Task<IEnumerable<ParkingGroupOutputModel>> Get();
    Task<ParkingGroupOutputModel> Create(ParkingGroupInputModel im);
  }
}

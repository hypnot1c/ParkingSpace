using System.Collections.Generic;
using System.Threading.Tasks;
using PS.Web.Api.Model.Input;
using PS.Web.Api.Model.Output.V1.Management;

namespace PS.Web.Api.Client.V1.Management
{
  public interface IParkingGroupsArea
  {
    Task<IEnumerable<ParkingGroupOutputModel>> Get();
    Task<ParkingGroupOutputModel> Create(ParkingGroupInputModel im);
  }
}

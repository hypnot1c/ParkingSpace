using AutoMapper;
using PS.Data.Master.Model;
using PS.Web.Api.Model.Output;

namespace PS.Web.Api.Versions.V1
{
  /// <summary>
  /// 
  /// </summary>
  public class ParkingGroupMappingProfile : Profile
  {
    /// <summary>
    /// 
    /// </summary>
    public ParkingGroupMappingProfile()
    {
      #region output
      CreateMap<ParkingGroupModel, ParkingGroupOutputModel>()
        ;
      #endregion

      #region mediator

      #endregion
    }
  }
}

using AutoMapper;
using PS.Data.Master.Model;
using PS.Web.Api.Model.Input;
using PS.Web.Api.Model.Output.V1.Management;

namespace PS.Web.Api.Versions.V1.Management
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
      #region input
      #endregion
      #region output
      CreateMap<ParkingGroupModel, ParkingGroupOutputModel>()
        ;
      #endregion

      #region mediator
      CreateMap<ParkingGroupInputModel, ParkingGroupCreateRequest>()
        ;

      CreateMap<ParkingGroupCreateRequest, ParkingGroupModel>()
        .ForMember(d => d.Id, opt => opt.Ignore())
        ;
      #endregion
    }
  }
}

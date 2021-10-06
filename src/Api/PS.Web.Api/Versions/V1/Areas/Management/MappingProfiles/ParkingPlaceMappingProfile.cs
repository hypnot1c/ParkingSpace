using AutoMapper;
using PS.Data.Master.Model;
using PS.Web.Api.Model.Output.V1.Management;

namespace PS.Web.Api.Versions.V1.Management
{
  /// <summary>
  /// 
  /// </summary>
  public class ParkingPlaceMappingProfile : Profile
  {
    /// <summary>
    /// 
    /// </summary>
    public ParkingPlaceMappingProfile()
    {
      #region output
      CreateMap<ParkingPlaceModel, ParkingPlaceOutputModel>()
        ;
      #endregion

      #region mediator

      #endregion
    }
  }
}

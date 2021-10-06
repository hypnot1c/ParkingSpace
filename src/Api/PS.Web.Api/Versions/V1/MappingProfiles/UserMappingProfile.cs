using AutoMapper;
using PS.Data.Master.Model;
using PS.Web.Api.Model.Input;
using PS.Web.Api.Model.Output.V1;

namespace PS.Web.Api.Versions.V1
{
  /// <summary>
  /// 
  /// </summary>
  public class UserMappingProfile : Profile
  {
    /// <summary>
    /// 
    /// </summary>
    public UserMappingProfile()
    {
      #region output
      CreateMap<UserModel, UserOutputModel>()
        ;
      #endregion

      #region mediator
      CreateMap<UserInputModel, UserEnsureCreatedRequest>()
        ;

      CreateMap<UserEnsureCreatedRequest, UserCreateRequest>()
        ;

      CreateMap<UserCreateRequest, UserModel>()
        .ForMember(d => d.Id, opt => opt.Ignore())
        .ForMember(d => d.AvatarUrl, opt => opt.Ignore())
        .ForMember(d => d.IsEnabled, opt => opt.Ignore())
        .ForMember(d => d.DateCreated, opt => opt.Ignore())
        ;
      #endregion
    }
  }
}

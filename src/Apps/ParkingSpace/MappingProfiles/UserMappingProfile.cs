using Nelibur.ObjectMapper;

namespace ParkingSpace.MappingProfiles
{
  class UserMappingProfile
  {
    public static void Init()
    {
      TinyMapper.Bind<Api.Google.Client.Model.Output.UserOutputModel, PS.Web.Api.Client.Model.Input.UserInputModel>(config =>
      {
        config.Bind(s => s.GivenName, d => d.Firstname);
        config.Bind(s => s.FamilyName, d => d.Lastname);
        config.Bind(s => s.Email, d => d.Email);
        config.Bind(s => s.PictureUrl, d => d.AvatarUrl);
        config.Ignore(s => s.Id);
        config.Ignore(s => s.Name);
      });
    }
  }
}

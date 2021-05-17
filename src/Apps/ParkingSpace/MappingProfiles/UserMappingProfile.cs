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

      TinyMapper.Bind<PS.Web.Api.Client.Model.Output.UserOutputModel, PS.Model.UserSessionModel>(config =>
      {
        config.Bind(s => s.Id, d => d.Id);
        config.Bind(s => s.Firstname, d => d.Firstname);
        config.Bind(s => s.Lastname, d => d.Lastname);
        config.Bind(s => s.Email, d => d.Email);
        config.Bind(s => s.AvatarUrl, d => d.AvatarUrl);
      });
    }
  }
}

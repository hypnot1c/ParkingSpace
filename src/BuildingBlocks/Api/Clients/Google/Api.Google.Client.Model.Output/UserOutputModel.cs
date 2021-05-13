using Newtonsoft.Json;

namespace Api.Google.Client.Model.Output
{
  public class UserOutputModel
  {
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    [JsonProperty(PropertyName = "email")]
    public string Email { get; set; }
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "given_name")]
    public string GivenName { get; set; }
    [JsonProperty(PropertyName = "family_name")]
    public string FamilyName { get; set; }
    [JsonProperty(PropertyName = "picture")]
    public string PictureUrl { get; set; }
  }
}

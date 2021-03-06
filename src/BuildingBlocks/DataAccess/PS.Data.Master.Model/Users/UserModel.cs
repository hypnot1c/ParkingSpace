using System;

namespace PS.Data.Master.Model
{
  public class UserModel : IdentityBaseModel
  {
    public UserModel()
    {
      this.IsEnabled = true;
      this.DateCreated = DateTime.UtcNow;
    }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string AvatarUrl { get; set; }
    public bool IsEnabled { get; set; }
    public DateTime DateCreated { get; set; }
  }
}

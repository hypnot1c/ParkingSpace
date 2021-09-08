using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PS.Data.Master.Model;

namespace PS.Data.Master.Context
{
  public class UserConfiguration : IEntityTypeConfiguration<UserModel>
  {
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
      builder.ToTable("User");

      builder.HasKey(p => p.Id);

      builder.Ignore(p => p.AvatarUrl);
    }
  }
}

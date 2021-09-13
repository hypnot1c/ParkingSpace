using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PS.Data.Master.Model;

namespace PS.Data.Master.Context
{
  public class ParkingGroupConfiguration : IEntityTypeConfiguration<ParkingGroupModel>
  {
    public void Configure(EntityTypeBuilder<ParkingGroupModel> builder)
    {
      builder.ToTable("Parking_Group");

      builder.HasKey(p => p.Id);
    }
  }
}

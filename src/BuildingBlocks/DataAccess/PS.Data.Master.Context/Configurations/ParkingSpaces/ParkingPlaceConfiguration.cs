using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PS.Data.Master.Model;

namespace PS.Data.Master.Context
{
  public class ParkingPlaceConfiguration : IEntityTypeConfiguration<ParkingPlaceModel>
  {
    public void Configure(EntityTypeBuilder<ParkingPlaceModel> builder)
    {
      builder.ToTable("Parking_Place");

      builder.HasKey(p => p.Id);
    }
  }
}

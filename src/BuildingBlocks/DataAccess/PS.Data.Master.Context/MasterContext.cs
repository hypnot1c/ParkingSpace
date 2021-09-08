using System.Reflection;
using Extensions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PS.Data.Master.Model;

namespace PS.Data.Master.Context
{
  public class MasterContext : DbContext
  {
    public MasterContext(DbContextOptions<MasterContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.UseEntityTypeConfiguration(typeof(MasterContext).GetTypeInfo().Assembly);
    }

    public DbSet<UserModel> Users { get; set; }
  }
}

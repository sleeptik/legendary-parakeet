using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ImiknWifiNavigationApp.IWNA.EF.Models
{
    public partial class IwnaContext : DbContext
    {
        public IwnaContext()
        {
        }

        public IwnaContext(DbContextOptions<IwnaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessPoint> AccessPoints { get; set; } = null!;
        public virtual DbSet<ApLocation> ApLocations { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(new SqliteConnectionStringBuilder
                {
                    DataSource = @$"{AppDomain.CurrentDomain.BaseDirectory}\iwna.sqlite3"
                }.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessPoint>(entity =>
            {
                entity.ToTable("access_points");

                entity.Property(e => e.AccessPointId).HasColumnName("access_point_id");

                entity.Property(e => e.Bssid).HasColumnName("bssid");

                entity.Property(e => e.Ssid).HasColumnName("ssid");
            });

            modelBuilder.Entity<ApLocation>(entity =>
            {
                entity.HasKey(e => new { e.AccessPointId, e.LocationId });

                entity.ToTable("ap_locations");

                entity.Property(e => e.AccessPointId).HasColumnName("access_point_id");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.SignalStrength).HasColumnName("signal_strength");

                entity.HasOne(d => d.AccessPoint)
                    .WithMany(p => p.ApLocations)
                    .HasForeignKey(d => d.AccessPointId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.ApLocations)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("locations");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Floor).HasColumnName("floor");

                entity.Property(e => e.PosX).HasColumnName("pos_x");

                entity.Property(e => e.PosY).HasColumnName("pos_y");

                entity.HasMany(d => d.PointAs)
                    .WithMany(p => p.PointBs)
                    .UsingEntity<Dictionary<string, object>>(
                        "LocationPath",
                        l => l.HasOne<Location>().WithMany().HasForeignKey("PointA")
                            .OnDelete(DeleteBehavior.ClientSetNull),
                        r => r.HasOne<Location>().WithMany().HasForeignKey("PointB")
                            .OnDelete(DeleteBehavior.ClientSetNull),
                        j =>
                        {
                            j.HasKey("PointA", "PointB");

                            j.ToTable("location_paths");

                            j.IndexerProperty<long>("PointA").HasColumnName("point_a");

                            j.IndexerProperty<long>("PointB").HasColumnName("point_b");
                        });

                entity.HasMany(d => d.PointBs)
                    .WithMany(p => p.PointAs)
                    .UsingEntity<Dictionary<string, object>>(
                        "LocationPath",
                        l => l.HasOne<Location>().WithMany().HasForeignKey("PointB")
                            .OnDelete(DeleteBehavior.ClientSetNull),
                        r => r.HasOne<Location>().WithMany().HasForeignKey("PointA")
                            .OnDelete(DeleteBehavior.ClientSetNull),
                        j =>
                        {
                            j.HasKey("PointA", "PointB");

                            j.ToTable("location_paths");

                            j.IndexerProperty<long>("PointA").HasColumnName("point_a");

                            j.IndexerProperty<long>("PointB").HasColumnName("point_b");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
using Microsoft.EntityFrameworkCore;
using Location = ImiknWifiNavigationApp.IWNA.Database.Models.Location;

namespace ImiknWifiNavigationApp.IWNA.Database.Models
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

        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Network> Networks { get; set; } = null!;
        public virtual DbSet<NetworkLocation> NetworkLocations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Data Source=D:\\iwna.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("locations");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Floor).HasColumnName("floor");

                entity.Property(e => e.PosX)
                    .HasColumnType("FLOAT")
                    .HasColumnName("pos_x");

                entity.Property(e => e.PosY)
                    .HasColumnType("FLOAT")
                    .HasColumnName("pos_y");

                entity.HasMany(d => d.EndNodes)
                    .WithMany(p => p.StartNodes)
                    .UsingEntity<Dictionary<string, object>>(
                        "LocationPath",
                        l => l.HasOne<Location>().WithMany().HasForeignKey("EndNode").OnDelete(DeleteBehavior.ClientSetNull),
                        r => r.HasOne<Location>().WithMany().HasForeignKey("StartNode").OnDelete(DeleteBehavior.ClientSetNull),
                        j =>
                        {
                            j.HasKey("StartNode", "EndNode");

                            j.ToTable("location_paths");

                            j.IndexerProperty<long>("StartNode").HasColumnName("start_node");

                            j.IndexerProperty<long>("EndNode").HasColumnName("end_node");
                        });

                entity.HasMany(d => d.StartNodes)
                    .WithMany(p => p.EndNodes)
                    .UsingEntity<Dictionary<string, object>>(
                        "LocationPath",
                        l => l.HasOne<Location>().WithMany().HasForeignKey("StartNode").OnDelete(DeleteBehavior.ClientSetNull),
                        r => r.HasOne<Location>().WithMany().HasForeignKey("EndNode").OnDelete(DeleteBehavior.ClientSetNull),
                        j =>
                        {
                            j.HasKey("StartNode", "EndNode");

                            j.ToTable("location_paths");

                            j.IndexerProperty<long>("StartNode").HasColumnName("start_node");

                            j.IndexerProperty<long>("EndNode").HasColumnName("end_node");
                        });
            });

            modelBuilder.Entity<Network>(entity =>
            {
                entity.ToTable("networks");

                entity.Property(e => e.NetworkId).HasColumnName("network_id");

                entity.Property(e => e.Bssid).HasColumnName("bssid");

                entity.Property(e => e.Ssid).HasColumnName("ssid");
            });

            modelBuilder.Entity<NetworkLocation>(entity =>
            {
                entity.HasKey(e => new { e.NetworkId, e.LocationId });

                entity.ToTable("network_locations");

                entity.Property(e => e.NetworkId).HasColumnName("network_id");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.SignalStrength)
                    .HasColumnType("FLOAT")
                    .HasColumnName("signal_strength");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.NetworkLocations)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Network)
                    .WithMany(p => p.NetworkLocations)
                    .HasForeignKey(d => d.NetworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using AllrideApiCore.Entities.Here;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllrideApiRepository.Configurations
{
    public class RouteTransportModeConfiguration:IEntityTypeConfiguration<RouteTransportMode>
    {

        public void Configure(EntityTypeBuilder<RouteTransportMode> builder)
        {
            builder.ToTable("route_transport_mode");
            builder.HasKey(p => p.Type);
            builder.Property(p => p.Type).HasColumnName("type");
            builder.Property(p => p.Mode).HasColumnName("mode");
            builder.HasOne(a => a.Route)
                .WithOne(b => b.RouteTransportMode)
                .HasForeignKey<Route>(b => b.RouteTransportModeId);
        }
    }
}

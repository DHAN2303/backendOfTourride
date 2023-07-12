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
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Mode).HasColumnName("mode");
        }
    }
}

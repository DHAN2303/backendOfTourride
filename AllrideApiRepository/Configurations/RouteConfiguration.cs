using AllrideApiCore.Entities.Here;
using AllrideApiCore.Entities.Routes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllrideApiRepository.Configurations
{
    public class RouteConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.ToTable("route");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.UserId).HasColumnName("user_id");
            builder.Property(p => p.Geoloc).HasColumnName("geoloc");
            builder.Property(p => p.OriginPoint).HasColumnName("origin_point");
            builder.Property(p => p.DestinationPoint).HasColumnName("destination_point");
            builder.Property(p => p.Waypoints).HasColumnName("waypoints");
            builder.Property(p => p.Length).HasColumnName("length");
            builder.Property(p => p.Duration).HasColumnName("duration");
            builder.Property(p => p.Public).HasColumnName("public");
            builder.Property(p => p.RouteTransportModeId).HasColumnName("transport_type");
            builder.Property(p => p.CreatedDate).HasColumnName("created_date");
            builder.Property(p => p.UpdatedDate).HasColumnName("updated_date");
            builder.Property(p => p.UpdatedDate).HasColumnName("updated_date");
            builder.Property(p => p.EditorAdvice).HasColumnName("editor_advice");

            builder.HasOne(a=>a.RouteInstruction)
                   .WithOne(b=>b.Route)
                   .HasForeignKey<RouteInstruction>(b=>b.RouteId);

            builder.HasOne(a => a.RouteDetail)
                   .WithOne(b => b.Route)
                   .HasForeignKey<RouteDetail>(b => b.RouteId);
        }
    }
}

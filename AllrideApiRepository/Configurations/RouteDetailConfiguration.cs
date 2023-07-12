using AllrideApiCore.Entities.Routes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllrideApiRepository.Configurations
{
    internal class RouteDetailsConfiguration : IEntityTypeConfiguration<RouteDetail>
    {

        public void Configure(EntityTypeBuilder<RouteDetail> builder)
        {

            builder.ToTable("route_detail");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.RouteId).HasColumnName("route_id");
            builder.Property(p => p.ImagePath).HasColumnName("image_path");
            builder.Property(p => p.ImageLocation).HasColumnName("image_location");
            builder.Property(p => p.AverageUphillSlope).HasColumnName("avg_uphill_slope");
            builder.Property(p => p.AverageDownhillSlope).HasColumnName("avg_downhill_slope");
            builder.Property(p => p.FavoriteCounter).HasColumnName("favorite_counter");
        }
    }        
}

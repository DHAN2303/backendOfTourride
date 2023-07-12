using AllrideApiCore.Entities.Weathers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllrideApiRepository.Configurations
{
    public class WeatherConfiguration : IEntityTypeConfiguration<Weather>
    {
        public void Configure(EntityTypeBuilder<Weather> builder)
        {
            builder.ToTable("weather");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Latitude).HasColumnName("latitude");
            builder.Property(p => p.Longitude).HasColumnName("longitude");
            builder.Property(p => p.Geoloc).HasColumnName("geoloc");
            builder.Property(p => p.Temperature).HasColumnName("temperature");
            builder.Property(p => p.Date).HasColumnName("datetime");
            builder.Property(p => p.CreatedDate).HasColumnName("created_time");
            builder.Property(p => p.UpdatedDate).HasColumnName("updated_time");
            builder.Property(p => p.Pressure).HasColumnName("pressure");
            builder.Property(p => p.SeaLevel).HasColumnName("sea_level");
            builder.Property(p => p.GrndLevel).HasColumnName("grnd_level");
            builder.Property(p => p.Humidity).HasColumnName("humidity");
            builder.Property(p => p.StatusId).HasColumnName("status_id");
            builder.Property(p => p.Predection3h).HasColumnName("predection_3h");
            builder.Property(p => p.WindSpeed).HasColumnName("wind_speed");
            builder.Property(p => p.WindDeg).HasColumnName("wind_deg");
            builder.Property(p => p.WindGust).HasColumnName("wind_gust");
            builder.Property(p => p.WeatherId).HasColumnName("weather_id");
           //builder.Property(p => p.WeatherParameter).HasColumnName("weather_parameter");
            builder.Property(p => p.WeatherDescription).HasColumnName("weather_description");
           // builder.Property(p => p.NightDay).HasColumnName("night_day");

        }
    }
}

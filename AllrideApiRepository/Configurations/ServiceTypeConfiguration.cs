using AllrideApiCore.Entities;
using AllrideApiCore.Entities.ServiceLimit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllrideApiRepository.Configurations
{
    public class ServiceTypeConfiguration : IEntityTypeConfiguration<ServiceTypes>
    {
        public void Configure(EntityTypeBuilder<ServiceTypes> builder)
        {
            builder.ToTable("service_types");
            builder.HasKey("service_id");
            builder.Property(p => p.service_id).HasColumnName("id");
            builder.Property(p => p.service_name).HasColumnName("service_name");
        }
    }
}

using AllrideApiCore.Entities.Here;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllrideApiRepository.Configurations
{
    public class RouteInstructionConfiguration : IEntityTypeConfiguration<RouteInstruction>
    {
        public void Configure(EntityTypeBuilder<RouteInstruction> builder)
        {
            builder.ToTable("route_instruction");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.RouteId).HasColumnName("route_id");
            builder.Property(p => p.Instruction).HasColumnName("instruction");
            builder.Property(p => p.Offset).HasColumnName("offset");
            builder.Property(p => p.Language).HasColumnName("language");
            builder.Property(p => p.CreatedDate).HasColumnName("created_date");
            builder.Property(p => p.UpdatedDate).HasColumnName("updated_date");

        }
    }
}

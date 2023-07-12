using AllrideApiCore.Entities.Here;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllrideApiRepository.Configurations
{
    internal class RouteInstructionDetailConfiguration : IEntityTypeConfiguration<RouteInstructionDetail>
    {
        public void Configure(EntityTypeBuilder<RouteInstructionDetail> builder)
        {
            builder.ToTable("route_instruction_detail");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.RouteId).HasColumnName("route_id");
            builder.Property(p => p.Leng).HasColumnName("length");
            builder.Property(p => p.Duration).HasColumnName("duration");
            builder.Property(p => p.Offset).HasColumnName("offset");
            builder.Property(p => p.Direction).HasColumnName("direction");
            builder.Property(p => p.Action).HasColumnName("action");
            builder.Property(p => p.Instruction).HasColumnName("instruction");
            builder.Property(p => p.Language).HasColumnName("language");
        }
    }
}

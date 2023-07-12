using AllrideApiCore.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllrideApiRepository.Configurations
{
    internal class UserDetailConfiguration : IEntityTypeConfiguration<UserDetail>
    {
        public void Configure(EntityTypeBuilder<UserDetail> builder)
        {
            builder.ToTable("user_detail");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.UserId).HasColumnName("user_id");
            builder.Property(p => p.Name).HasColumnName("name");
            builder.Property(p => p.LastName).HasColumnName("lastname");
            builder.Property(p => p.DateOfBirth).HasColumnName("date_of_birth");
            builder.Property(p => p.Gender).HasColumnName("gender");
            builder.Property(p => p.Phone).HasColumnName("phone");
            builder.Property(p => p.Country).HasColumnName("country");
            builder.Property(p => p.Language).HasColumnName("language");
            builder.Property(p => p.VehicleType).HasColumnName("vehicle_type");
            builder.Property(p => p.PpPath).HasColumnName("pp_path");
            builder.Property(p => p.CreatedDate).HasColumnName("created_date");
            builder.Property(p => p.UpdatedDate).HasColumnName("updated_date");
         
        }
    }
}

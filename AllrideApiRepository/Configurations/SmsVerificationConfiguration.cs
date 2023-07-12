using AllrideApiCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllrideApiRepository.Configurations
{
    public class SmsVerificationConfiguration : IEntityTypeConfiguration<SmsVerification>
    {
        public void Configure(EntityTypeBuilder<SmsVerification> builder)
        {
            builder.ToTable("sms_verification");
            builder.HasKey(x=> x.Id);
            builder.Property(x => x.Id).HasColumnName("id");    
            builder.Property(x=>x.Code).HasColumnName("verification_code");
            builder.Property(p => p.UserId).HasColumnName("user_id");

        }
    }
}

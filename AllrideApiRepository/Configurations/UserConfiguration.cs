using AllrideApiCore.Entities;
using AllrideApiCore.Entities.Here;
using AllrideApiCore.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllrideApiRepository.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {

            builder.ToTable("user");
            builder.HasKey(x => x.Id).HasName("user_pkey"); 
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Email).HasColumnName("email");
            builder.Property(p => p.FacebookId).HasColumnName("facebook_id");
            builder.Property(p => p.GoogleId).HasColumnName("google_id");
            builder.Property(p => p.InstagramId).HasColumnName("instagram_id");
            builder.Property(p => p.AppleId).HasColumnName("apple_id");
            builder.Property(p => p.VerifiedMember).HasColumnName("verified_member");
            builder.Property(p => p.CreatedDate).HasColumnName("created_date");
            builder.Property(p => p.UpdatedDate).HasColumnName("updated_date");
            builder.Property(p => p.DeletedDate).HasColumnName("deleted_date");
            builder.Property(p => p.ActiveUser).HasColumnName("active_user");
            builder.HasOne(a => a.UserDetail)
                .WithOne(b => b.User)
                .HasForeignKey<UserDetail>(b => b.UserId);
            builder.HasOne(a => a.UserPassword)
                .WithOne(b => b.User)
                .HasForeignKey<UserPassword>(b => b.UserId);
            builder.HasOne(x => x.SmsVerification)
                .WithOne(x=>x.User)
                .HasForeignKey<SmsVerification>(b => b.UserId);
            builder.HasOne(x => x.Route)
                .WithOne(x => x.User)
                .HasForeignKey<Route>(b => b.UserId);
        }
    }        
}
//builder.Entity<User>(userItem =>
// {
//     userItem.HasKey(e => e.Id).HasName("user_pkey");
//     userItem.HasIndex(e => e.Id).IsUnique();
//     userItem.Property(p => p.Id)
//      .HasColumnName("id");
//     userItem.Property(p => p.Email)
//     .HasColumnName("email");
//     userItem.HasOne(a => a.userdetail)
//      .WithOne(b => b.user)
//      .HasForeignKey<UserDetail>(b => b.user_id);
//     userItem.HasOne(p => p.userpassword)
//     .WithOne(p => p.user)
//     .HasForeignKey<UserPassword>(p => p.user_id);
// });
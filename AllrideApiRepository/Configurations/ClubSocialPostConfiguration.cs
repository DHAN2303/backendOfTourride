using AllrideApiCore.Entities.Clubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllrideApiRepository.Configurations
{
    public class ClubSocialPostConfiguration : IEntityTypeConfiguration<ClubSocialPost>
    {
        public void Configure(EntityTypeBuilder<ClubSocialPost> builder)
        {

            builder.ToTable("clubsocial_post");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.ClubMemberId).HasColumnName("club_member_id");
            builder.Property(p => p.ClubId).HasColumnName("club_id");
            builder.Property(p => p.PostImagePath).HasColumnName("post_image_path");
            builder.Property(p => p.HashTag).HasColumnName("hash_tag");
            builder.Property(p => p.LikeUnlikeCount).HasColumnName("likeUnlike_count");
            builder.Property(p => p.CreatedDate).HasColumnName("created_date");
            builder.Property(p => p.UpdatedDate).HasColumnName("updated_date");
            builder.Property(p => p.DeletedDate).HasColumnName("deleted_date");

            builder.HasMany(csp => csp.ClubSocialPostComment)
             .WithOne(cspc => cspc.ClubSocialPost)
             .HasForeignKey(cspc => cspc.ClubSocialPostId);

            builder.HasOne(csp => csp.Club)
            .WithMany(cspc => cspc.ClubSocialPost)
            .HasForeignKey(cspc => cspc.ClubId);

            builder.HasOne(csp => csp.ClubMember)
                .WithMany(csp=>csp.ClubSocialPost)
                .HasForeignKey(csp=>csp.ClubMemberId);


        }
    }
}

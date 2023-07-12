using AllrideApiCore.Entities.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllrideApiRepository.Configurations
{
    public class GroupSocialPostConfiguration : IEntityTypeConfiguration<GroupSocialPost>
    {
        public void Configure(EntityTypeBuilder<GroupSocialPost> builder)
        {

            builder.ToTable("groupsocial_post");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.GroupMemberId).HasColumnName("group_member_id");
            builder.Property(p => p.GroupId).HasColumnName("group_id");
            builder.Property(p => p.PostImagePath).HasColumnName("post_image_path");
            builder.Property(p => p.HashTag).HasColumnName("hash_tag");
            builder.Property(p => p.LikeUnlikeCount).HasColumnName("likeUnlike_count");
            builder.Property(p => p.CreatedDate).HasColumnName("created_date");
            builder.Property(p => p.UpdatedDate).HasColumnName("updated_date");
            builder.Property(p => p.DeletedDate).HasColumnName("deleted_date");
            builder.HasMany(csp => csp.GroupSocialPostComment)
             .WithOne(cspc => cspc.GroupSocialPost)
             .HasForeignKey(cspc => cspc.GroupSocialPostId);

            builder.HasOne(csp => csp.Group)
            .WithMany(cspc => cspc.GroupSocialPost)
            .HasForeignKey(cspc => cspc.GroupId);

            builder.HasOne(csp => csp.GroupMember)
                .WithMany(csp => csp.GroupSocialPost)
                .HasForeignKey(csp => csp.GroupMemberId);
        }
    }
}

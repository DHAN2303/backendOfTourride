using AllrideApiCore.Entities.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllrideApiRepository.Configurations
{
    public class GroupSocialPostCommentConfiguration : IEntityTypeConfiguration<GroupSocialPostComment>
    {
        public void Configure(EntityTypeBuilder<GroupSocialPostComment> builder)
        {

            builder.ToTable("groupsocial_postcomment");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.GroupMemberId).HasColumnName("groupMember_id");
            builder.Property(p => p.GroupSocialPostId).HasColumnName("groupsocial_postid");
            builder.Property(p => p.Comment).HasColumnName("comment");
            builder.Property(p => p.CreatedDate).HasColumnName("created_date");
            builder.Property(p => p.UpdatedDate).HasColumnName("updated_date");
            builder.Property(p => p.DeletedDate).HasColumnName("deleted_date");
          

        }
    }
}

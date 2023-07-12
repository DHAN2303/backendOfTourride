using AllrideApiCore.Entities.Clubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllrideApiRepository.Configurations
{
    public class ClubSocialPostCommentConfiguration : IEntityTypeConfiguration<ClubSocialPostComment>
    {
        public void Configure(EntityTypeBuilder<ClubSocialPostComment> builder)
        {

            builder.ToTable("clubsocial_postcomment");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.ClubMemberId).HasColumnName("clubMember_id");
            builder.Property(p => p.ClubSocialPostId).HasColumnName("clubsocial_postid");
            builder.Property(p => p.Comment).HasColumnName("comment");
            builder.Property(p => p.CreatedDate).HasColumnName("created_date");
            builder.Property(p => p.UpdatedDate).HasColumnName("updated_date");
            builder.Property(p => p.DeletedDate).HasColumnName("deleted_date");
          

        }
    }
}

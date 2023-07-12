using AllrideApiCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllrideApiRepository.Configurations
{
    internal class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.ToTable("news");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Title).HasColumnName("title");
            builder.Property(p => p.Description).HasColumnName("description");
            builder.Property(p => p.Image).HasColumnName("image");
            builder.Property(p => p.UserId).HasColumnName("authorId");
            builder.Property(p => p.LikeCount).HasColumnName("likeCount");
            builder.Property(p => p.DislikeCount).HasColumnName("dislikeCount");
            builder.Property(p => p.CreatedDate).HasColumnName("createdAt");
            builder.Property(p => p.UpdatedDate).HasColumnName("updatedAt");
        
        }
    }        
}

using AllrideApiCore.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllrideApiRepository.Configurations
{
    internal class UserNewsReactionConfiguration:IEntityTypeConfiguration<UserNewsReaction>
    {
        public void Configure(EntityTypeBuilder<UserNewsReaction> builder)
        {
            builder.ToTable("user_news_reaction");
            builder.HasKey(e => e.Id);
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.ActionType).HasColumnName("action_type");
            builder.Property(p => p.UserId).HasColumnName("user_id");
            builder.Property(p => p.NewsId).HasColumnName("news_id");
            builder.Property(p => p.CreatedDate).HasColumnName("created_date");
            builder.Property(p => p.UpdatedDate).HasColumnName("updated_date");
            
        }
    }
}

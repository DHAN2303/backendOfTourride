using System.Reflection;
using AllrideApiCore.Entities;
using AllrideApiCore.Entities.Activities;
using AllrideApiCore.Entities.Buys;
using AllrideApiCore.Entities.Chat;
using AllrideApiCore.Entities.Clubs;
using AllrideApiCore.Entities.Commons;
using AllrideApiCore.Entities.Groups;
using AllrideApiCore.Entities.Here;
using AllrideApiCore.Entities.Newss;
using AllrideApiCore.Entities.RoutePlanners;
using AllrideApiCore.Entities.Routes;
using AllrideApiCore.Entities.ServiceLimit;
using AllrideApiCore.Entities.SocialMedia;
using AllrideApiCore.Entities.Users;
using AllrideApiCore.Entities.Weathers;
using AllrideApiRepository.Configurations;
using AllrideApiRepository.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;

namespace AllrideApiRepository
{
    public class AllrideApiDbContext : DbContext
    {

        // for User
        public DbSet<UserDetail> user_detail { get; set; }
        public DbSet<UserEntity> user { get; set; }
        public DbSet<UserPassword> user_password { get; set; }
        public DbSet<OnlineUsers> online_users { get; set; }
        public DbSet<SmsVerification> smsVerification { get; set; }
        public DbSet<UserBlock> user_block { get; set; }
        public DbSet<UserInvites> user_invites { get; set; }

        // for News
        public DbSet<News> news { get; set; }
        public DbSet<UserNewsReaction> user_news_reaction { get; set; }
        public DbSet<NewsTags> news_tags { get; set; }

        // for Route
        public DbSet<RouteCalculate> route_calculate { get; set; }
        public DbSet<Route> route { get; set; }
        public DbSet<RouteTransportMode> route_transport_mode { get; set; }
        public DbSet<RouteInstruction> route_instruction { get; set; }
        public DbSet<RouteInstructionDetail> route_instruction_detail { get; set; }
        public DbSet<RouteDetail> route_detail { get; set; }
        public DbSet<RoutePlanner> route_planner { get; set; }
        public DbSet<UsersInRoutePlanning> users_InRoutePlanning { get; set; }
        public DbSet<TasksRoutePlanner> tasks_route_planner { get; set; }

        // for Weather
        public DbSet<Weather> weather { get; set; }

        //for chat
        public DbSet<Message> messages { get; set; }
        public DbSet<Group> groups { get; set; }
        public DbSet<GroupMessage> group_messages { get; set; }
        public DbSet<GroupMember> group_member { get; set; }

        //for club
        public DbSet<Club> club { get; set; }
        public DbSet<ClubMember> club_member { get; set; }
        public DbSet<ClubSocialPost> clubsocial_post { get; set; }
        public DbSet<ClubSocialPostComment> clubsocial_postcomment { get; set; }
        //public DbSet<GroupMessage> group_messages { get; set; }

        //for log
        public DbSet<LogApi> api_log { get; set; }

        // group social media
        public DbSet<GroupSocialPost> groupsocial_post { get; set; }
        public DbSet<GroupSocialPostComment> groupsocial_postcomment { get; set; }


        //for social media
        public DbSet<SocialMediaPosts> social_media_posts { get; set; }
        public DbSet<SocialMediaLike> social_media_likes { get; set; }
        public DbSet<SocialMediaComments> social_media_comments { get; set; }
        public DbSet<SocialMediaFollow> social_media_follows { get; set; }
        public DbSet<SocialMediaStory> social_media_story { get; set; }

        //for limit
        public DbSet<ServiceUsage> service_usage { get; set; }
        public DbSet<ServiceTypes> service_types { get; set; }
        public DbSet<UserTypes> user_types { get; set; }

        //for routes
        public DbSet<MyRoutes> user_my_route { get; set; }
        public DbSet<PublishedRoutes> user_shared_route { get; set; }
        public DbSet<FavouriteRoutes> user_favorite_route { get; set; }

        // Buy
        public DbSet<TouridePackage> touridePackage { get; set; }
        public DbSet<Activity> activity { get; set; }

        //Notification person
        public DbSet<PersonNotification> person_notification { get; set; }
        public DbSet<PersonNotificationMuteSettings> person_notification_mute_settings { get; set; }

        //Notification group
        public DbSet<GroupNotification> group_notification { get; set; }
        public DbSet<GroupNotificationMuteSettings> group_notification_mute_settings { get; set; }

        //Notification club
        public DbSet<ClubNotification> club_notification { get; set; }
        public DbSet<ClubNotificationMuteSettings> club_notification_mute_settings { get; set; }

        //Notification 
        public DbSet<NotificationTimeCatch> notification_time_catch { get; set; }
        public AllrideApiDbContext(DbContextOptions<AllrideApiDbContext> option) : base(option)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<User>()
            //    .HasMany(x => x.UserNewsReaction)
            //    .WithMany(x => x.Users)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "UserNewsReaction",
            //        x => x.HasOne<User>().WithMany().HasForeignKey("User_Id").HasConstraintName("FK_UserId"),
            //        x => x.HasOne<UserNewsReaction>().HasForeignKey("UserNewsReaction_Id").HasConstraintName("FK_UserNewsReactionId")
            //    );
            // Çalışmış olduğum assembly tarayarak tablolar için oluşturduğum configure classlarını tarayarak getirir.


            builder.Entity<UserNewsReaction>()
                        .HasKey(o => new { o.NewsId, o.UserId });

            builder.Entity<UserNewsReaction>()
                .HasOne<UserEntity>(o => o.User)
                .WithMany(c => c.UserNewsReactions)
                .HasForeignKey(o => o.UserId);

            builder.Entity<UserNewsReaction>()
                .HasOne<News>(o => o.News)
                .WithMany(c => c.UserNewsReactions)
                .HasForeignKey(o => o.NewsId);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // tek bir tanesi için
            builder.ApplyConfiguration(new UserPasswordConfiguration());

            builder.Entity<ServiceTypes>().ToTable("service_types");
            builder.Entity<ServiceTypes>().HasKey(s => s.service_id);
            builder.Entity<ServiceTypes>().Property(s => s.service_id).HasColumnName("service_id");
            builder.Entity<ServiceTypes>().Property(s => s.service_name).HasColumnName("service_name");

            builder.Entity<ServiceUsage>().HasKey(e => new { e.user_id, e.service_id });
            builder.Entity<UserTypes>().HasKey(e => e.type);

            builder.Entity<FavouriteRoutes>()
            .HasNoKey();
            builder.Entity<MyRoutes>()
            .HasNoKey();

            builder.Entity<PublishedRoutes>()
            .HasNoKey();

            builder.Entity<Group>().ToTable("group");

            //UserBlock table///////////////////////
            builder.Entity<UserBlock>()
                .HasKey(b => new { b.BlockingUserId, b.BlockedUserId });

            builder.Entity<UserBlock>()
                .HasOne<UserEntity>(b => b.UserBlocking)
                .WithMany(u => u.BlockingUserBlocks)
                .HasForeignKey(b => b.BlockingUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserBlock>()
                .HasOne<UserEntity>(b => b.UserBlocked)
                .WithMany(u => u.BlockedUserBlocks)
                .HasForeignKey(b => b.BlockedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            //UserInvites table///////////////////
            builder.Entity<UserInvites>()
                .HasKey(b => new { b.inveting_id, b.invited_id });

            builder.Entity<UserInvites>()
                .HasOne<UserEntity>(b => b.UserInviting)
                .WithMany(u => u.InvitingUser)
                .HasForeignKey(b => b.inveting_id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserInvites>()
                .HasOne<UserEntity>(b => b.UserInvited)
                .WithMany(u => u.InvitedUser)
                .HasForeignKey(b => b.invited_id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Activity>()
                .HasOne<Group>(o => o.Group)
                .WithMany(c => c.activities)
                .HasForeignKey(o => o.group_id);

            builder.Entity<Activity>()
                   .HasOne<Club>(o => o.Club)
                   .WithMany(c => c.activities)
                   .HasForeignKey(o => o.club_id);

            builder.Entity<Activity>()
                   .HasOne<UserEntity>(o => o.Users)
                   .WithOne(c => c.act)
                   .HasForeignKey<Activity>(o => o.creator_user_id);

            builder.Entity<Activity>()
                   .HasOne<Route>(o => o.Route)
                   .WithOne(c => c.Activity)
                   .HasForeignKey<Activity>(o => o.route_id);

            builder.Entity<ActivityMember>()
                   .HasOne<Activity>(x => x.Activitiy)
                   .WithMany(x => x.ActivityMembers)
                   .HasForeignKey(x => x.ActivityId);

            builder.Entity<ServiceUsage>()
                   .HasOne<UserEntity>(x => x.User)
                   .WithMany(x => x.ServiceUsages)
                   .HasForeignKey(x => x.user_id);

            builder.Entity<ServiceUsage>()
                   .HasOne<ServiceTypes>(x => x.ServiceTypes)
                   .WithMany(x => x.service_usages)
                   .HasForeignKey(x => x.service_id);

            builder.Entity<MemberShip>()
                  .HasOne<UserEntity>(x => x.User)
                  .WithOne(x => x.MemberShip)
                  .HasForeignKey<MemberShip>(x => x.UserId);

            builder.Entity<RouteAltitude>()
                   .HasOne<Route>(x => x.Route)
                   .WithMany(x => x.RouteAltitudes)
                   .HasForeignKey(x => x.RouteId);
            
            builder.Entity<RoutePlanner>()
                   .HasOne<UserEntity>(x => x.UserEntities)
                   .WithMany(x => x.RoutePlanners)
                   .HasForeignKey(x => x.UserId);


            builder.Entity<TasksRoutePlanner>()
                .HasOne<RoutePlanner>(o => o.RoutePlanner)
                .WithMany(c => c.TasksRoutePlanners)
                .HasForeignKey(o => o.RoutePlannerId);


            builder.Entity<Route>()
                .HasOne<RouteTransportMode>(o => o.RouteTransportMode)
                .WithMany()
                .HasForeignKey(o => o.RouteTransportModeId);

        }
    }
}

//builder.Entity<RoutePlanner>()
//    .HasOne<Group>(x => x.Group)
//    .WithMany(x => x.RoutePlanners)
//    .HasForeignKey(x => x.GroupId);

//builder.Entity<RoutePlanner>()
//    .HasOne<Club>(x => x.Club)
//    .WithMany(x => x.RoutePlanners)
//    .HasForeignKey(x => x.ClubId);

//builder.Entity<UsersInRoutePlanning>()
//            .HasOne(u => u.TasksRoutePlanner)
//            .WithOne(t => t.UsersInRoutePlanning)
//            .HasForeignKey<TasksRoutePlanner>(t => t.UsersInRoutePlanningId);

//builder.HasPostgresExtension("postgis");
//builder.Entity<UserDetail>().ToTable("user_detail");
//builder.Entity<RouteCalculate>().ToTable("routecalculates");
//builder.Entity<UserPassword>().Property(p => p.Id).HasColumnName("id");
//builder.Entity<UserDetail>().Property(p => p.Id).HasColumnName("id");

// RELATION SHIPS
// builder.Entity<User>()
//.HasOne(a => a.userpassword)
//.WithOne(b => b.user)
//.HasForeignKey<UserPassword>(b => b.user_id);

//builder.Entity<User>()
// .HasOne(a => a.userdetail)
// .WithOne(b => b.user)
// .HasForeignKey<UserDetail>(b => b.user_id);

//builder.Entity<RouteCalculate>().Property(c => c.srid)
//.HasDefaultValue(3857);

//builder.Entity<UserDetail>()
//       .Property(ud => ud.user_id).IsRequired(true);

// Her bir class library assemblydir. Bu assembly içerisinden tüm configuration dosyalarını okuyor.
// Oluşturduğum tüm Configurationlar IEntityConfiguration dosyalarını implemente ettiği için reflection yaparak bu inteface e sahip tüm dosyaları okuyor.
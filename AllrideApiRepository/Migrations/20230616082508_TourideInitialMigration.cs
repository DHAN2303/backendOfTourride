using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AllrideApiRepository.Migrations
{
    /// <inheritdoc />
    public partial class TourideInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "api_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_ip = table.Column<string>(type: "text", nullable: true),
                    service_name = table.Column<string>(type: "text", nullable: true),
                    request_param = table.Column<string>(type: "text", nullable: true),
                    response_status = table.Column<int>(type: "integer", nullable: false),
                    response = table.Column<string>(type: "text", nullable: true),
                    api_type = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    response_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_api_log", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "club_notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clubId = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    sendDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_club_notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "club_notification_mute_settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clubId = table.Column<int>(type: "integer", nullable: false),
                    isMute = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_club_notification_mute_settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "group_messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    group_id = table.Column<int>(type: "integer", nullable: false),
                    sender_id = table.Column<int>(type: "integer", nullable: false),
                    message_content = table.Column<string>(type: "text", nullable: true),
                    content_type = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "group_notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    groupId = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    sendDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "group_notification_mute_settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    groupId = table.Column<int>(type: "integer", nullable: false),
                    isMute = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_notification_mute_settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sender_id = table.Column<int>(type: "integer", nullable: false),
                    recipient_id = table.Column<int>(type: "integer", nullable: false),
                    content_type = table.Column<int>(type: "integer", nullable: false),
                    message_content = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notification_time_catch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    notification_id = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<bool>(type: "boolean", nullable: false),
                    sendDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_time_catch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "online_users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_online_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "person_notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "text", nullable: true),
                    sendDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "person_notification_mute_settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    isMute = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person_notification_mute_settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "route_calculate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    geom = table.Column<Geometry>(type: "geometry", nullable: true),
                    geom2 = table.Column<Geometry[]>(type: "geometry[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_route_calculate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "route_instruction_detail",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    route_id = table.Column<int>(type: "integer", nullable: false),
                    length = table.Column<int[]>(type: "integer[]", nullable: true),
                    duration = table.Column<int[]>(type: "integer[]", nullable: true),
                    offset = table.Column<int[]>(type: "integer[]", nullable: true),
                    direction = table.Column<string[]>(type: "text[]", nullable: true),
                    action = table.Column<string[]>(type: "text[]", nullable: true),
                    instruction = table.Column<string[]>(type: "text[]", nullable: true),
                    language = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_route_instruction_detail", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "route_transport_mode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_route_transport_mode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "service_types",
                columns: table => new
                {
                    service_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    service_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service_types", x => x.service_id);
                });

            migrationBuilder.CreateTable(
                name: "social_media_comments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    post_id = table.Column<int>(type: "integer", nullable: false),
                    text = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_social_media_comments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "social_media_follows",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    follower_id = table.Column<int>(type: "integer", nullable: false),
                    followed_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_social_media_follows", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "social_media_likes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    post_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_social_media_likes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "social_media_posts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    caption = table.Column<string>(type: "text", nullable: true),
                    media_url = table.Column<string>(type: "text", nullable: true),
                    location_info = table.Column<string>(type: "text", nullable: true),
                    likes_count = table.Column<int>(type: "integer", nullable: true),
                    comments_count = table.Column<int>(type: "integer", nullable: true),
                    LikedByUsers = table.Column<List<int>>(type: "integer[]", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_social_media_posts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "social_media_story",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    caption = table.Column<string>(type: "text", nullable: true),
                    media_url = table.Column<string>(type: "text", nullable: false),
                    location_info = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_social_media_story", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "touridePackage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_touridePackage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: true),
                    facebook_id = table.Column<int>(type: "integer", nullable: false),
                    google_id = table.Column<int>(type: "integer", nullable: false),
                    instagram_id = table.Column<int>(type: "integer", nullable: false),
                    apple_id = table.Column<int>(type: "integer", nullable: false),
                    verified_member = table.Column<bool>(type: "boolean", nullable: false),
                    forgot_password_code = table.Column<int>(type: "integer", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    ResfreshTokenEndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    user_type = table.Column<int>(type: "integer", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    active_user = table.Column<bool>(type: "boolean", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_favorite_route",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    route_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "user_my_route",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    route_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "user_shared_route",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    route_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "user_types",
                columns: table => new
                {
                    type = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tomtom_nearby_limit = table.Column<int>(type: "integer", nullable: false),
                    tomtom_along_limit = table.Column<int>(type: "integer", nullable: false),
                    here_nearby_limit = table.Column<int>(type: "integer", nullable: false),
                    tomtom_routing_limit = table.Column<int>(type: "integer", nullable: false),
                    weather_limit = table.Column<int>(type: "integer", nullable: false),
                    here_route_limit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_types", x => x.type);
                });

            migrationBuilder.CreateTable(
                name: "weather",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    geoloc = table.Column<Geometry>(type: "geometry", nullable: true),
                    temperature = table.Column<double>(type: "double precision", nullable: false),
                    datetime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    pressure = table.Column<int>(type: "integer", nullable: false),
                    sea_level = table.Column<int>(type: "integer", nullable: false),
                    grnd_level = table.Column<int>(type: "integer", nullable: false),
                    humidity = table.Column<int>(type: "integer", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    predection_3h = table.Column<double>(type: "double precision", nullable: false),
                    wind_speed = table.Column<int>(type: "integer", nullable: false),
                    wind_deg = table.Column<int>(type: "integer", nullable: false),
                    wind_gust = table.Column<int>(type: "integer", nullable: false),
                    weather_id = table.Column<int>(type: "integer", nullable: false),
                    weather_description = table.Column<string>(type: "text", nullable: true),
                    created_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weather", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "club",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    CreatorId = table.Column<int>(type: "integer", nullable: false),
                    backgroundCover_path = table.Column<string>(type: "text", nullable: false),
                    profile_path = table.Column<string>(type: "text", nullable: false),
                    image_path = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    is_invite = table.Column<int>(type: "integer", nullable: false),
                    group_members = table.Column<List<string>>(type: "text[]", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_club", x => x.Id);
                    table.ForeignKey(
                        name: "FK_club_user_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "group",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatorId = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    image_path = table.Column<string>(type: "text", nullable: true),
                    backgroundCover_path = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    group_rank = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    is_invite = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group", x => x.id);
                    table.ForeignKey(
                        name: "FK_group_user_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberShip",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    MemberShipType = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberShip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberShip_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "news",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    authorId = table.Column<int>(type: "integer", nullable: false),
                    image = table.Column<string>(type: "text", nullable: true),
                    likeCount = table.Column<int>(type: "integer", nullable: false),
                    dislikeCount = table.Column<int>(type: "integer", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_news", x => x.id);
                    table.ForeignKey(
                        name: "FK_news_user_authorId",
                        column: x => x.authorId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "route",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    geoloc = table.Column<Geometry>(type: "geometry", nullable: true),
                    origin_point = table.Column<Geometry>(type: "geometry", nullable: true),
                    destination_point = table.Column<Geometry>(type: "geometry", nullable: true),
                    duration = table.Column<double>(type: "double precision", nullable: false),
                    length = table.Column<double>(type: "double precision", nullable: false),
                    transport_type = table.Column<int>(type: "integer", nullable: false),
                    editor_advice = table.Column<int>(type: "integer", nullable: false),
                    @public = table.Column<string>(name: "public", type: "text", nullable: true),
                    IsRoutePlanner = table.Column<bool>(type: "boolean", nullable: false),
                    waypoints = table.Column<Geometry>(type: "geometry", nullable: true),
                    RouteTransportModeId1 = table.Column<int>(type: "integer", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_route", x => x.id);
                    table.ForeignKey(
                        name: "FK_route_route_transport_mode_RouteTransportModeId1",
                        column: x => x.RouteTransportModeId1,
                        principalTable: "route_transport_mode",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_route_route_transport_mode_transport_type",
                        column: x => x.transport_type,
                        principalTable: "route_transport_mode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_route_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "service_usage",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    service_id = table.Column<int>(type: "integer", nullable: false),
                    usage_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service_usage", x => new { x.user_id, x.service_id });
                    table.ForeignKey(
                        name: "FK_service_usage_service_types_service_id",
                        column: x => x.service_id,
                        principalTable: "service_types",
                        principalColumn: "service_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_service_usage_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sms_verification",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    verification_code = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sms_verification", x => x.id);
                    table.ForeignKey(
                        name: "FK_sms_verification_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_block",
                columns: table => new
                {
                    BlockedUserId = table.Column<int>(type: "integer", nullable: false),
                    BlockingUserId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_block", x => new { x.BlockingUserId, x.BlockedUserId });
                    table.ForeignKey(
                        name: "FK_user_block_user_BlockedUserId",
                        column: x => x.BlockedUserId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_block_user_BlockingUserId",
                        column: x => x.BlockingUserId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_detail",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    lastname = table.Column<string>(type: "text", nullable: true),
                    date_of_birth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    gender = table.Column<byte>(type: "smallint", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: true),
                    country = table.Column<string>(type: "text", nullable: true),
                    language = table.Column<string>(type: "text", nullable: true),
                    pp_path = table.Column<string>(type: "text", nullable: true),
                    vehicle_type = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_detail_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_invites",
                columns: table => new
                {
                    inveting_id = table.Column<int>(type: "integer", nullable: false),
                    invited_id = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    where = table.Column<int>(type: "integer", nullable: false),
                    whereId = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false),
                    invitedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_invites", x => new { x.inveting_id, x.invited_id });
                    table.ForeignKey(
                        name: "FK_user_invites_user_inveting_id",
                        column: x => x.inveting_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_invites_user_invited_id",
                        column: x => x.invited_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_password",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    salt_pass = table.Column<byte[]>(type: "bytea", nullable: true),
                    hash_pass = table.Column<byte[]>(type: "bytea", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_password", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_password_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "club_member",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClubId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    club_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    active = table.Column<int>(type: "integer", nullable: false),
                    joined_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_club_member", x => x.id);
                    table.ForeignKey(
                        name: "FK_club_member_club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_club_member_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "group_member",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    group_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    joined_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    active = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_member", x => x.id);
                    table.ForeignKey(
                        name: "FK_group_member_group_group_id",
                        column: x => x.group_id,
                        principalTable: "group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NewsId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    TagsId = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsTags_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NewsTags_news_NewsId",
                        column: x => x.NewsId,
                        principalTable: "news",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_news_reaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    action_type = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    news_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_news_reaction", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_news_reaction_news_news_id",
                        column: x => x.news_id,
                        principalTable: "news",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_news_reaction_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "activity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    creator_user_id = table.Column<int>(type: "integer", nullable: false),
                    group_id = table.Column<int>(type: "integer", nullable: false),
                    club_id = table.Column<int>(type: "integer", nullable: false),
                    route_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_activity_club_club_id",
                        column: x => x.club_id,
                        principalTable: "club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_activity_group_group_id",
                        column: x => x.group_id,
                        principalTable: "group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_activity_route_route_id",
                        column: x => x.route_id,
                        principalTable: "route",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_activity_user_creator_user_id",
                        column: x => x.creator_user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "route_detail",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    route_id = table.Column<int>(type: "integer", nullable: false),
                    image_path = table.Column<string>(type: "text", nullable: true),
                    image_location = table.Column<string>(type: "text", nullable: true),
                    avg_uphill_slope = table.Column<double>(type: "double precision", nullable: false),
                    avg_downhill_slope = table.Column<double>(type: "double precision", nullable: false),
                    favorite_counter = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_route_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_route_detail_route_route_id",
                        column: x => x.route_id,
                        principalTable: "route",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "route_instruction",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    route_id = table.Column<int>(type: "integer", nullable: false),
                    instruction = table.Column<string>(type: "text", nullable: true),
                    offset = table.Column<int>(type: "integer", nullable: false),
                    language = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_route_instruction", x => x.id);
                    table.ForeignKey(
                        name: "FK_route_instruction_route_route_id",
                        column: x => x.route_id,
                        principalTable: "route",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "route_planner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RouteId = table.Column<int>(type: "integer", nullable: false),
                    RoutePlannerTitle = table.Column<string>(type: "text", nullable: true),
                    RouteName = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ColorCodeHex = table.Column<string>(type: "text", nullable: true),
                    RouteAlertTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_route_planner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_route_planner_route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "route",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_route_planner_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouteAltitude",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RouteId = table.Column<int>(type: "integer", nullable: false),
                    Geoloc = table.Column<Geometry>(type: "geometry", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteAltitude", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteAltitude_route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "route",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clubsocial_post",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    club_id = table.Column<int>(type: "integer", nullable: false),
                    club_member_id = table.Column<int>(type: "integer", nullable: false),
                    post_image_path = table.Column<string[]>(type: "text[]", nullable: true),
                    hash_tag = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    likeUnlike_count = table.Column<int>(type: "integer", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clubsocial_post", x => x.id);
                    table.ForeignKey(
                        name: "FK_clubsocial_post_club_club_id",
                        column: x => x.club_id,
                        principalTable: "club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_clubsocial_post_club_member_club_member_id",
                        column: x => x.club_member_id,
                        principalTable: "club_member",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "groupsocial_post",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    group_id = table.Column<int>(type: "integer", nullable: false),
                    group_member_id = table.Column<int>(type: "integer", nullable: false),
                    post_image_path = table.Column<string[]>(type: "text[]", nullable: true),
                    hash_tag = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    likeUnlike_count = table.Column<int>(type: "integer", nullable: false),
                    deleted_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groupsocial_post", x => x.id);
                    table.ForeignKey(
                        name: "FK_groupsocial_post_group_group_id",
                        column: x => x.group_id,
                        principalTable: "group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_groupsocial_post_group_member_group_member_id",
                        column: x => x.group_member_id,
                        principalTable: "group_member",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityMember",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActivityId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityMember_activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "activity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tasks_route_planner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoutePlannerId = table.Column<int>(type: "integer", nullable: false),
                    Tasks = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks_route_planner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tasks_route_planner_route_planner_RoutePlannerId",
                        column: x => x.RoutePlannerId,
                        principalTable: "route_planner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users_InRoutePlanning",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoutePlannerId = table.Column<int>(type: "integer", nullable: false),
                    SocialMediaFoll = table.Column<int>(type: "integer", nullable: false),
                    TasksId = table.Column<List<int>>(type: "integer[]", nullable: true),
                    SocialMediaFollowid = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_InRoutePlanning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_InRoutePlanning_route_planner_RoutePlannerId",
                        column: x => x.RoutePlannerId,
                        principalTable: "route_planner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_InRoutePlanning_social_media_follows_SocialMediaFollo~",
                        column: x => x.SocialMediaFollowid,
                        principalTable: "social_media_follows",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "clubsocial_postcomment",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clubMember_id = table.Column<int>(type: "integer", nullable: false),
                    clubsocial_postid = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clubsocial_postcomment", x => x.id);
                    table.ForeignKey(
                        name: "FK_clubsocial_postcomment_club_member_clubMember_id",
                        column: x => x.clubMember_id,
                        principalTable: "club_member",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_clubsocial_postcomment_clubsocial_post_clubsocial_postid",
                        column: x => x.clubsocial_postid,
                        principalTable: "clubsocial_post",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "groupsocial_postcomment",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    groupMember_id = table.Column<int>(type: "integer", nullable: false),
                    groupsocial_postid = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groupsocial_postcomment", x => x.id);
                    table.ForeignKey(
                        name: "FK_groupsocial_postcomment_group_member_groupMember_id",
                        column: x => x.groupMember_id,
                        principalTable: "group_member",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_groupsocial_postcomment_groupsocial_post_groupsocial_postid",
                        column: x => x.groupsocial_postid,
                        principalTable: "groupsocial_post",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_activity_club_id",
                table: "activity",
                column: "club_id");

            migrationBuilder.CreateIndex(
                name: "IX_activity_creator_user_id",
                table: "activity",
                column: "creator_user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_activity_group_id",
                table: "activity",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_activity_route_id",
                table: "activity",
                column: "route_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityMember_ActivityId",
                table: "ActivityMember",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_club_CreatorId",
                table: "club",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_club_member_ClubId",
                table: "club_member",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_club_member_UserId",
                table: "club_member",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_clubsocial_post_club_id",
                table: "clubsocial_post",
                column: "club_id");

            migrationBuilder.CreateIndex(
                name: "IX_clubsocial_post_club_member_id",
                table: "clubsocial_post",
                column: "club_member_id");

            migrationBuilder.CreateIndex(
                name: "IX_clubsocial_postcomment_clubMember_id",
                table: "clubsocial_postcomment",
                column: "clubMember_id");

            migrationBuilder.CreateIndex(
                name: "IX_clubsocial_postcomment_clubsocial_postid",
                table: "clubsocial_postcomment",
                column: "clubsocial_postid");

            migrationBuilder.CreateIndex(
                name: "IX_group_CreatorId",
                table: "group",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_group_member_group_id",
                table: "group_member",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_groupsocial_post_group_id",
                table: "groupsocial_post",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_groupsocial_post_group_member_id",
                table: "groupsocial_post",
                column: "group_member_id");

            migrationBuilder.CreateIndex(
                name: "IX_groupsocial_postcomment_groupMember_id",
                table: "groupsocial_postcomment",
                column: "groupMember_id");

            migrationBuilder.CreateIndex(
                name: "IX_groupsocial_postcomment_groupsocial_postid",
                table: "groupsocial_postcomment",
                column: "groupsocial_postid");

            migrationBuilder.CreateIndex(
                name: "IX_MemberShip_UserId",
                table: "MemberShip",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_news_authorId",
                table: "news",
                column: "authorId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsTags_NewsId",
                table: "NewsTags",
                column: "NewsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsTags_TagsId",
                table: "NewsTags",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_route_RouteTransportModeId1",
                table: "route",
                column: "RouteTransportModeId1");

            migrationBuilder.CreateIndex(
                name: "IX_route_transport_type",
                table: "route",
                column: "transport_type");

            migrationBuilder.CreateIndex(
                name: "IX_route_user_id",
                table: "route",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_route_detail_route_id",
                table: "route_detail",
                column: "route_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_route_instruction_route_id",
                table: "route_instruction",
                column: "route_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_route_planner_RouteId",
                table: "route_planner",
                column: "RouteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_route_planner_UserId",
                table: "route_planner",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteAltitude_RouteId",
                table: "RouteAltitude",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_service_usage_service_id",
                table: "service_usage",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "IX_sms_verification_user_id",
                table: "sms_verification",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tasks_route_planner_RoutePlannerId",
                table: "tasks_route_planner",
                column: "RoutePlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_user_block_BlockedUserId",
                table: "user_block",
                column: "BlockedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_detail_user_id",
                table: "user_detail",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_invites_invited_id",
                table: "user_invites",
                column: "invited_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_news_reaction_news_id",
                table: "user_news_reaction",
                column: "news_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_news_reaction_user_id",
                table: "user_news_reaction",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_password_user_id",
                table: "user_password",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_InRoutePlanning_RoutePlannerId",
                table: "users_InRoutePlanning",
                column: "RoutePlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_users_InRoutePlanning_SocialMediaFollowid",
                table: "users_InRoutePlanning",
                column: "SocialMediaFollowid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityMember");

            migrationBuilder.DropTable(
                name: "api_log");

            migrationBuilder.DropTable(
                name: "club_notification");

            migrationBuilder.DropTable(
                name: "club_notification_mute_settings");

            migrationBuilder.DropTable(
                name: "clubsocial_postcomment");

            migrationBuilder.DropTable(
                name: "group_messages");

            migrationBuilder.DropTable(
                name: "group_notification");

            migrationBuilder.DropTable(
                name: "group_notification_mute_settings");

            migrationBuilder.DropTable(
                name: "groupsocial_postcomment");

            migrationBuilder.DropTable(
                name: "MemberShip");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "NewsTags");

            migrationBuilder.DropTable(
                name: "notification_time_catch");

            migrationBuilder.DropTable(
                name: "online_users");

            migrationBuilder.DropTable(
                name: "person_notification");

            migrationBuilder.DropTable(
                name: "person_notification_mute_settings");

            migrationBuilder.DropTable(
                name: "route_calculate");

            migrationBuilder.DropTable(
                name: "route_detail");

            migrationBuilder.DropTable(
                name: "route_instruction");

            migrationBuilder.DropTable(
                name: "route_instruction_detail");

            migrationBuilder.DropTable(
                name: "RouteAltitude");

            migrationBuilder.DropTable(
                name: "service_usage");

            migrationBuilder.DropTable(
                name: "sms_verification");

            migrationBuilder.DropTable(
                name: "social_media_comments");

            migrationBuilder.DropTable(
                name: "social_media_likes");

            migrationBuilder.DropTable(
                name: "social_media_posts");

            migrationBuilder.DropTable(
                name: "social_media_story");

            migrationBuilder.DropTable(
                name: "tasks_route_planner");

            migrationBuilder.DropTable(
                name: "touridePackage");

            migrationBuilder.DropTable(
                name: "user_block");

            migrationBuilder.DropTable(
                name: "user_detail");

            migrationBuilder.DropTable(
                name: "user_favorite_route");

            migrationBuilder.DropTable(
                name: "user_invites");

            migrationBuilder.DropTable(
                name: "user_my_route");

            migrationBuilder.DropTable(
                name: "user_news_reaction");

            migrationBuilder.DropTable(
                name: "user_password");

            migrationBuilder.DropTable(
                name: "user_shared_route");

            migrationBuilder.DropTable(
                name: "user_types");

            migrationBuilder.DropTable(
                name: "users_InRoutePlanning");

            migrationBuilder.DropTable(
                name: "weather");

            migrationBuilder.DropTable(
                name: "activity");

            migrationBuilder.DropTable(
                name: "clubsocial_post");

            migrationBuilder.DropTable(
                name: "groupsocial_post");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "service_types");

            migrationBuilder.DropTable(
                name: "news");

            migrationBuilder.DropTable(
                name: "route_planner");

            migrationBuilder.DropTable(
                name: "social_media_follows");

            migrationBuilder.DropTable(
                name: "club_member");

            migrationBuilder.DropTable(
                name: "group_member");

            migrationBuilder.DropTable(
                name: "route");

            migrationBuilder.DropTable(
                name: "club");

            migrationBuilder.DropTable(
                name: "group");

            migrationBuilder.DropTable(
                name: "route_transport_mode");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}

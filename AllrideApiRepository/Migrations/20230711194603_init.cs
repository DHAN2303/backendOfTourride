using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AllrideApiRepository.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                name: "club_member",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    club_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    joined_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_club_member", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "club_messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    club_id = table.Column<int>(type: "integer", nullable: false),
                    sender_id = table.Column<int>(type: "integer", nullable: false),
                    message_content = table.Column<string>(type: "text", nullable: true),
                    content_type = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_club_messages", x => x.id);
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
                name: "clubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    image_path = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    club_rank = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clubs", x => x.Id);
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
                    joined_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_member", x => x.id);
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
                name: "groups",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    image_path = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    group_rank = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groups", x => x.id);
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
                    type = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_route_transport_mode", x => x.type);
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
                name: "service_usage",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    service_id = table.Column<int>(type: "integer", nullable: false),
                    usage_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service_usage", x => new { x.user_id, x.service_id });
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
                    media_url = table.Column<string>(type: "text", nullable: true),
                    location_info = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_social_media_story", x => x.id);
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
                    verified_member = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("user_pkey", x => x.id);
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
                    waypoints = table.Column<Geometry>(type: "geometry", nullable: true),
                    @public = table.Column<string>(name: "public", type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_route", x => x.id);
                    table.ForeignKey(
                        name: "FK_route_route_transport_mode_transport_type",
                        column: x => x.transport_type,
                        principalTable: "route_transport_mode",
                        principalColumn: "type",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_route_user_user_id",
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
                    gender = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    country = table.Column<string>(type: "text", nullable: true),
                    language = table.Column<string>(type: "text", nullable: true),
                    pp_path = table.Column<string>(type: "text", nullable: true),
                    vehicle_type = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<bool>(type: "boolean", nullable: false),
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    inveting_id = table.Column<int>(type: "integer", nullable: false),
                    invited_id = table.Column<int>(type: "integer", nullable: false),
                    where = table.Column<int>(type: "integer", nullable: false),
                    whereId = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false),
                    invitedDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_invites", x => new { x.inveting_id, x.invited_id, x.Id });
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

            migrationBuilder.CreateIndex(
                name: "IX_news_authorId",
                table: "news",
                column: "authorId");

            migrationBuilder.CreateIndex(
                name: "IX_route_transport_type",
                table: "route",
                column: "transport_type",
                unique: true);

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
                name: "IX_sms_verification_user_id",
                table: "sms_verification",
                column: "user_id",
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "api_log");

            migrationBuilder.DropTable(
                name: "club_member");

            migrationBuilder.DropTable(
                name: "club_messages");

            migrationBuilder.DropTable(
                name: "club_notification");

            migrationBuilder.DropTable(
                name: "club_notification_mute_settings");

            migrationBuilder.DropTable(
                name: "clubs");

            migrationBuilder.DropTable(
                name: "group_member");

            migrationBuilder.DropTable(
                name: "group_messages");

            migrationBuilder.DropTable(
                name: "group_notification");

            migrationBuilder.DropTable(
                name: "group_notification_mute_settings");

            migrationBuilder.DropTable(
                name: "groups");

            migrationBuilder.DropTable(
                name: "messages");

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
                name: "service_types");

            migrationBuilder.DropTable(
                name: "service_usage");

            migrationBuilder.DropTable(
                name: "sms_verification");

            migrationBuilder.DropTable(
                name: "social_media_comments");

            migrationBuilder.DropTable(
                name: "social_media_follows");

            migrationBuilder.DropTable(
                name: "social_media_likes");

            migrationBuilder.DropTable(
                name: "social_media_posts");

            migrationBuilder.DropTable(
                name: "social_media_story");

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
                name: "weather");

            migrationBuilder.DropTable(
                name: "route");

            migrationBuilder.DropTable(
                name: "news");

            migrationBuilder.DropTable(
                name: "route_transport_mode");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}

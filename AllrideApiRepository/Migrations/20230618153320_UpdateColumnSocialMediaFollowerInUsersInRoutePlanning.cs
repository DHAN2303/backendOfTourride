using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllrideApiRepository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnSocialMediaFollowerInUsersInRoutePlanning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_route_planner_route_RouteId",
                table: "route_planner");

            migrationBuilder.DropIndex(
                name: "IX_route_planner_RouteId",
                table: "route_planner");

            migrationBuilder.RenameColumn(
                name: "SocialMediaFoll",
                table: "users_InRoutePlanning",
                newName: "SocialMediaFollower");

            migrationBuilder.AddColumn<int>(
                name: "RoutePlannerId",
                table: "route",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_route_RoutePlannerId",
                table: "route",
                column: "RoutePlannerId");

            migrationBuilder.AddForeignKey(
                name: "FK_route_route_planner_RoutePlannerId",
                table: "route",
                column: "RoutePlannerId",
                principalTable: "route_planner",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_route_route_planner_RoutePlannerId",
                table: "route");

            migrationBuilder.DropIndex(
                name: "IX_route_RoutePlannerId",
                table: "route");

            migrationBuilder.DropColumn(
                name: "RoutePlannerId",
                table: "route");

            migrationBuilder.RenameColumn(
                name: "SocialMediaFollower",
                table: "users_InRoutePlanning",
                newName: "SocialMediaFoll");

            migrationBuilder.CreateIndex(
                name: "IX_route_planner_RouteId",
                table: "route_planner",
                column: "RouteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_route_planner_route_RouteId",
                table: "route_planner",
                column: "RouteId",
                principalTable: "route",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

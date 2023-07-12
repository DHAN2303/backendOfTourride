using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllrideApiRepository.Migrations
{
    /// <inheritdoc />
    public partial class TourideRouteAndUserTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_route_user_id",
                table: "route");

            migrationBuilder.CreateIndex(
                name: "IX_route_user_id",
                table: "route",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_route_user_id",
                table: "route");

            migrationBuilder.CreateIndex(
                name: "IX_route_user_id",
                table: "route",
                column: "user_id",
                unique: true);
        }
    }
}

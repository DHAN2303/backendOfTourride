using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllrideApiRepository.Migrations
{
    /// <inheritdoc />
    public partial class AddNewsTagsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsTags_Tags_TagsId",
                table: "NewsTags");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsTags_news_NewsId",
                table: "NewsTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsTags",
                table: "NewsTags");

            migrationBuilder.RenameTable(
                name: "NewsTags",
                newName: "news_tags");

            migrationBuilder.RenameIndex(
                name: "IX_NewsTags_TagsId",
                table: "news_tags",
                newName: "IX_news_tags_TagsId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsTags_NewsId",
                table: "news_tags",
                newName: "IX_news_tags_NewsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_news_tags",
                table: "news_tags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_news_tags_Tags_TagsId",
                table: "news_tags",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_news_tags_news_NewsId",
                table: "news_tags",
                column: "NewsId",
                principalTable: "news",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_news_tags_Tags_TagsId",
                table: "news_tags");

            migrationBuilder.DropForeignKey(
                name: "FK_news_tags_news_NewsId",
                table: "news_tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_news_tags",
                table: "news_tags");

            migrationBuilder.RenameTable(
                name: "news_tags",
                newName: "NewsTags");

            migrationBuilder.RenameIndex(
                name: "IX_news_tags_TagsId",
                table: "NewsTags",
                newName: "IX_NewsTags_TagsId");

            migrationBuilder.RenameIndex(
                name: "IX_news_tags_NewsId",
                table: "NewsTags",
                newName: "IX_NewsTags_NewsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsTags",
                table: "NewsTags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsTags_Tags_TagsId",
                table: "NewsTags",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsTags_news_NewsId",
                table: "NewsTags",
                column: "NewsId",
                principalTable: "news",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

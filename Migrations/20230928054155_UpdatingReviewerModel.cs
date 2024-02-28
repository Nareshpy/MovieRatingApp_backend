using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRatingApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingReviewerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Reviewers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Reviewers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Reviewers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Reviewers");
        }
    }
}

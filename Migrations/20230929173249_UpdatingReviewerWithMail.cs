using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRatingApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingReviewerWithMail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Reviewers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Reviewers");
        }
    }
}

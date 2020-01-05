using Microsoft.EntityFrameworkCore.Migrations;

namespace TestCoreApp.Migrations
{
    public partial class FixUserSurveyAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSurveyAnswers_AspNetUsers_UserIdId",
                table: "UserSurveyAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserSurveyAnswers_UserIdId",
                table: "UserSurveyAnswers");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "UserSurveyAnswers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserSurveyAnswers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSurveyAnswers_UserId",
                table: "UserSurveyAnswers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSurveyAnswers_AspNetUsers_UserId",
                table: "UserSurveyAnswers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSurveyAnswers_AspNetUsers_UserId",
                table: "UserSurveyAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserSurveyAnswers_UserId",
                table: "UserSurveyAnswers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserSurveyAnswers");

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "UserSurveyAnswers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSurveyAnswers_UserIdId",
                table: "UserSurveyAnswers",
                column: "UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSurveyAnswers_AspNetUsers_UserIdId",
                table: "UserSurveyAnswers",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

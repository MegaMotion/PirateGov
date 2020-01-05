using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestCoreApp.Migrations
{
    public partial class SurveyDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Survey",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "Survey",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Survey",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Survey",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Survey",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "SurveyQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sort = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    BaseSurveyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyQuestion_Survey_BaseSurveyId",
                        column: x => x.BaseSurveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SurveyAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sort = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    surveyId = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyAnswer_SurveyQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "SurveyQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SurveyAnswer_Survey_surveyId",
                        column: x => x.surveyId,
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswer_QuestionId",
                table: "SurveyAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswer_surveyId",
                table: "SurveyAnswer",
                column: "surveyId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestion_BaseSurveyId",
                table: "SurveyQuestion",
                column: "BaseSurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyAnswer");

            migrationBuilder.DropTable(
                name: "SurveyQuestion");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "Survey");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Survey");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Survey");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Survey");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Survey",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}

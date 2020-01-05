using Microsoft.EntityFrameworkCore.Migrations;

namespace TestCoreApp.Migrations
{
    public partial class UserSurveyAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyAnswers_SurveyQuestions_QuestionId",
                table: "SurveyAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyAnswers_Surveys_surveyId",
                table: "SurveyAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyAnswers",
                table: "SurveyAnswers");

            migrationBuilder.RenameTable(
                name: "SurveyAnswers",
                newName: "SurveyAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyAnswers_surveyId",
                table: "SurveyAnswer",
                newName: "IX_SurveyAnswer_surveyId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyAnswers_QuestionId",
                table: "SurveyAnswer",
                newName: "IX_SurveyAnswer_QuestionId");

            migrationBuilder.AddColumn<bool>(
                name: "useAnswerDescriptions",
                table: "Surveys",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "useQuestionDescriptions",
                table: "Surveys",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyAnswer",
                table: "SurveyAnswer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyAnswer_SurveyQuestions_QuestionId",
                table: "SurveyAnswer",
                column: "QuestionId",
                principalTable: "SurveyQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyAnswer_Surveys_surveyId",
                table: "SurveyAnswer",
                column: "surveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyAnswer_SurveyQuestions_QuestionId",
                table: "SurveyAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyAnswer_Surveys_surveyId",
                table: "SurveyAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyAnswer",
                table: "SurveyAnswer");

            migrationBuilder.DropColumn(
                name: "useAnswerDescriptions",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "useQuestionDescriptions",
                table: "Surveys");

            migrationBuilder.RenameTable(
                name: "SurveyAnswer",
                newName: "SurveyAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyAnswer_surveyId",
                table: "SurveyAnswers",
                newName: "IX_SurveyAnswers_surveyId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyAnswer_QuestionId",
                table: "SurveyAnswers",
                newName: "IX_SurveyAnswers_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyAnswers",
                table: "SurveyAnswers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyAnswers_SurveyQuestions_QuestionId",
                table: "SurveyAnswers",
                column: "QuestionId",
                principalTable: "SurveyQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyAnswers_Surveys_surveyId",
                table: "SurveyAnswers",
                column: "surveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace TestCoreApp.Migrations
{
    public partial class surveyToSurveys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyAnswer_SurveyQuestion_QuestionId",
                table: "SurveyAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyAnswer_Survey_surveyId",
                table: "SurveyAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestion_Survey_BaseSurveyId",
                table: "SurveyQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyQuestion",
                table: "SurveyQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyAnswer",
                table: "SurveyAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Survey",
                table: "Survey");

            migrationBuilder.RenameTable(
                name: "SurveyQuestion",
                newName: "SurveyQuestions");

            migrationBuilder.RenameTable(
                name: "SurveyAnswer",
                newName: "SurveyAnswers");

            migrationBuilder.RenameTable(
                name: "Survey",
                newName: "Surveys");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestion_BaseSurveyId",
                table: "SurveyQuestions",
                newName: "IX_SurveyQuestions_BaseSurveyId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyAnswer_surveyId",
                table: "SurveyAnswers",
                newName: "IX_SurveyAnswers_surveyId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyAnswer_QuestionId",
                table: "SurveyAnswers",
                newName: "IX_SurveyAnswers_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyQuestions",
                table: "SurveyQuestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyAnswers",
                table: "SurveyAnswers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Surveys",
                table: "Surveys",
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

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Surveys_BaseSurveyId",
                table: "SurveyQuestions",
                column: "BaseSurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyAnswers_SurveyQuestions_QuestionId",
                table: "SurveyAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyAnswers_Surveys_surveyId",
                table: "SurveyAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Surveys_BaseSurveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Surveys",
                table: "Surveys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyQuestions",
                table: "SurveyQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyAnswers",
                table: "SurveyAnswers");

            migrationBuilder.RenameTable(
                name: "Surveys",
                newName: "Survey");

            migrationBuilder.RenameTable(
                name: "SurveyQuestions",
                newName: "SurveyQuestion");

            migrationBuilder.RenameTable(
                name: "SurveyAnswers",
                newName: "SurveyAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestions_BaseSurveyId",
                table: "SurveyQuestion",
                newName: "IX_SurveyQuestion_BaseSurveyId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyAnswers_surveyId",
                table: "SurveyAnswer",
                newName: "IX_SurveyAnswer_surveyId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyAnswers_QuestionId",
                table: "SurveyAnswer",
                newName: "IX_SurveyAnswer_QuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Survey",
                table: "Survey",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyQuestion",
                table: "SurveyQuestion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyAnswer",
                table: "SurveyAnswer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyAnswer_SurveyQuestion_QuestionId",
                table: "SurveyAnswer",
                column: "QuestionId",
                principalTable: "SurveyQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyAnswer_Survey_surveyId",
                table: "SurveyAnswer",
                column: "surveyId",
                principalTable: "Survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestion_Survey_BaseSurveyId",
                table: "SurveyQuestion",
                column: "BaseSurveyId",
                principalTable: "Survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Md_exercise.Migrations
{
    public partial class AddTrainingAmountRefreshingStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE dbo.RefreshTrainingAmount
                @TrainingAmount bigint 
                AS
                BEGIN
                    UPDATE Heroes
                    SET TrainingAmountPerformedToday = @TrainingAmount
                END");
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.RefreshTrainingAmount ");
        }
    }
}

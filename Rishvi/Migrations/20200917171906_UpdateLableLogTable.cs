using Microsoft.EntityFrameworkCore.Migrations;

namespace Rishvi.Migrations
{
    public partial class UpdateLableLogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LabelLogs",
                table: "LabelLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabelLogs",
                table: "LabelLogs",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LabelLogs",
                table: "LabelLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabelLogs",
                table: "LabelLogs",
                column: "GenerateLabelId");
        }
    }
}

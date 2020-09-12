using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rishvi.Migrations
{
    public partial class labelcount_childlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GenerateLabelCount",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Token = table.Column<string>(nullable: true),
                    Orderid = table.Column<string>(nullable: true),
                    Orderreference = table.Column<string>(nullable: true),
                    Logs = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Labelid = table.Column<string>(nullable: true),
                    Iserror = table.Column<bool>(nullable: false),
                    Error = table.Column<string>(nullable: true),
                    Linnrequest = table.Column<string>(nullable: true),
                    Linnresponse = table.Column<string>(nullable: true),
                    DHLrequest = table.Column<string>(nullable: true),
                    DHLresponse = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenerateLabelCount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabelLogs",
                columns: table => new
                {
                    GenerateLabelId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Log = table.Column<string>(nullable: true),
                    GeneratelabelLogId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelLogs", x => x.GenerateLabelId);
                    table.ForeignKey(
                        name: "FK_LabelLogs_GeneratelabelLog_GeneratelabelLogId",
                        column: x => x.GeneratelabelLogId,
                        principalTable: "GeneratelabelLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabelLogs_GeneratelabelLogId",
                table: "LabelLogs",
                column: "GeneratelabelLogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenerateLabelCount");

            migrationBuilder.DropTable(
                name: "LabelLogs");
        }
    }
}

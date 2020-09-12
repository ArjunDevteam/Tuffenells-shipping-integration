using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rishvi.Migrations
{
    public partial class generatelabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneratelabelLog",
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
                    table.PrimaryKey("PK_GeneratelabelLog", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneratelabelLog");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rishvi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "linnUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    LinnToken = table.Column<string>(nullable: true),
                    MaxImagesPerStockItem = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    DatabaseName = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    SuperAdmin = table.Column<bool>(nullable: false),
                    SessionUserId = table.Column<int>(nullable: false),
                    Device = table.Column<string>(nullable: true),
                    DeviceType = table.Column<string>(nullable: true),
                    Server = table.Column<string>(nullable: true),
                    PushServer = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    UserType = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    EntityId = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true),
                    Locality = table.Column<string>(nullable: true),
                    Md5Hash = table.Column<string>(nullable: true),
                    IsAccountHolder = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    isMainUser = table.Column<bool>(nullable: false),
                    isTrial = table.Column<bool>(nullable: false),
                    noOfUser = table.Column<int>(nullable: false),
                    type = table.Column<string>(nullable: true),
                    subscribedOn = table.Column<DateTime>(nullable: false),
                    DaysLeft = table.Column<int>(nullable: false),
                    isSubscribed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_linnUser", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "linnUser");
        }
    }
}

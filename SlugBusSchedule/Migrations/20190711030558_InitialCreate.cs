using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SlugBusSchedule.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BusNumber = table.Column<int>(nullable: false),
                    Street = table.Column<string>(nullable: true),
                    Base = table.Column<DateTime>(nullable: false),
                    Village = table.Column<DateTime>(nullable: false),
                    EastRemote = table.Column<DateTime>(nullable: false),
                    BookStore = table.Column<DateTime>(nullable: false),
                    CrownMerill = table.Column<DateTime>(nullable: false),
                    NineTen = table.Column<DateTime>(nullable: false),
                    ScienceHill = table.Column<DateTime>(nullable: false),
                    Kresge = table.Column<DateTime>(nullable: false),
                    PorterRachelCarson = table.Column<DateTime>(nullable: false),
                    Oakes = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules");
        }
    }
}

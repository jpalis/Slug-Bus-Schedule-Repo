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
                    BusNumber = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    Base = table.Column<string>(nullable: true),
                    Village = table.Column<string>(nullable: true),
                    EastRemote = table.Column<string>(nullable: true),
                    BookStore = table.Column<string>(nullable: true),
                    CrownMerill = table.Column<string>(nullable: true),
                    NineTen = table.Column<string>(nullable: true),
                    ScienceHill = table.Column<string>(nullable: true),
                    Kresge = table.Column<string>(nullable: true),
                    PorterRachelCarson = table.Column<string>(nullable: true),
                    Oakes = table.Column<string>(nullable: true)
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

using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentEducationBoardService.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    SchoolId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolName = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    CommunicationLanguage = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true),
                    Program = table.Column<string>(nullable: true),
                    AssessmentPeriod = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.SchoolId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}

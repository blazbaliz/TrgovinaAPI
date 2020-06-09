using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TrgovinaAPI.Migrations
{
    public partial class AddIzdelkiToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Izdelki",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImeIzdelka = table.Column<string>(nullable: true),
                    OpisIzdelka = table.Column<string>(nullable: true),
                    CenaIzdelka = table.Column<double>(nullable: false),
                    Zaloga = table.Column<int>(nullable: false),
                    NaVoljo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izdelki", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Izdelki");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webapi.Migrations
{
    public partial class CasaDeShowAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CasasDeShow",
                columns: table => new
                {
                    CasaDeShowId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Endereco = table.Column<string>(nullable: true),
                    NomeDaCasa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasasDeShow", x => x.CasaDeShowId);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    EventoId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeDoEvento = table.Column<string>(nullable: true),
                    CapacidadeDoEvento = table.Column<int>(nullable: false),
                    QuantidadeDeIngressos = table.Column<int>(nullable: false),
                    DataDoEvento = table.Column<DateTime>(nullable: false),
                    ValorDoIngresso = table.Column<double>(nullable: false),
                    GeneroDoEvento = table.Column<string>(nullable: true),
                    CasaDeShowId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.EventoId);
                    table.ForeignKey(
                        name: "FK_Eventos_CasasDeShow_CasaDeShowId",
                        column: x => x.CasaDeShowId,
                        principalTable: "CasasDeShow",
                        principalColumn: "CasaDeShowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_CasaDeShowId",
                table: "Eventos",
                column: "CasaDeShowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "CasasDeShow");
        }
    }
}

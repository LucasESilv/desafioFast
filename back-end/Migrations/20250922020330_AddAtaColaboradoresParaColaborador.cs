using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rastreamentoWorkshopAPI.Migrations
{
    public partial class AddAtaColaboradoresParaColaborador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colaboradores_Atas_AtaWorkshopId",
                table: "Colaboradores");

            migrationBuilder.DropIndex(
                name: "IX_Colaboradores_AtaWorkshopId",
                table: "Colaboradores");

            migrationBuilder.DropColumn(
                name: "AtaWorkshopId",
                table: "Colaboradores");

            migrationBuilder.CreateTable(
                name: "AtaColaboradores",
                columns: table => new
                {
                    AtaWorkshopId = table.Column<int>(type: "int", nullable: false),
                    ColaboradorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtaColaboradores", x => new { x.AtaWorkshopId, x.ColaboradorId });
                    table.ForeignKey(
                        name: "FK_AtaColaboradores_Atas_AtaWorkshopId",
                        column: x => x.AtaWorkshopId,
                        principalTable: "Atas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtaColaboradores_Colaboradores_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaboradores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtaColaboradores_ColaboradorId",
                table: "AtaColaboradores",
                column: "ColaboradorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtaColaboradores");

            migrationBuilder.AddColumn<int>(
                name: "AtaWorkshopId",
                table: "Colaboradores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_AtaWorkshopId",
                table: "Colaboradores",
                column: "AtaWorkshopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Colaboradores_Atas_AtaWorkshopId",
                table: "Colaboradores",
                column: "AtaWorkshopId",
                principalTable: "Atas",
                principalColumn: "Id");
        }
    }
}

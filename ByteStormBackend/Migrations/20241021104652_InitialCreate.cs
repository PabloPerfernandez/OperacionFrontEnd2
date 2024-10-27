using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteStormBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operativos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Rol = table.Column<string>(type: "TEXT", nullable: false),
                    MisionAsignada = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operativos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Misiones",
                columns: table => new
                {
                    codigo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    Estado = table.Column<int>(type: "INTEGER", nullable: false),
                    OperativoID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Misiones", x => x.codigo);
                    table.ForeignKey(
                        name: "FK_Misiones_Operativos_OperativoID",
                        column: x => x.OperativoID,
                        principalTable: "Operativos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipos",
                columns: table => new
                {
                    equipoCodigo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    equipoTipo = table.Column<int>(type: "INTEGER", nullable: false),
                    equipoDescripcion = table.Column<string>(type: "TEXT", nullable: true),
                    EstadoEquipo = table.Column<int>(type: "INTEGER", nullable: false),
                    MisionID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipos", x => x.equipoCodigo);
                    table.ForeignKey(
                        name: "FK_Equipos_Misiones_MisionID",
                        column: x => x.MisionID,
                        principalTable: "Misiones",
                        principalColumn: "codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_MisionID",
                table: "Equipos",
                column: "MisionID");

            migrationBuilder.CreateIndex(
                name: "IX_Misiones_OperativoID",
                table: "Misiones",
                column: "OperativoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipos");

            migrationBuilder.DropTable(
                name: "Misiones");

            migrationBuilder.DropTable(
                name: "Operativos");
        }
    }
}

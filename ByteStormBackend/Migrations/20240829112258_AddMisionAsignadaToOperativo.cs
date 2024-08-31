using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteStormBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddMisionAsignadaToOperativo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MisionAsignada",
                table: "Operativos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MisionAsignada",
                table: "Operativos");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PiolAPIS_Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddNuevasColumnasDocumentation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EndpointEspecifico",
                table: "documentations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "documentations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MensajesError",
                table: "documentations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Parametros",
                table: "documentations",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndpointEspecifico",
                table: "documentations");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "documentations");

            migrationBuilder.DropColumn(
                name: "MensajesError",
                table: "documentations");

            migrationBuilder.DropColumn(
                name: "Parametros",
                table: "documentations");
        }
    }
}

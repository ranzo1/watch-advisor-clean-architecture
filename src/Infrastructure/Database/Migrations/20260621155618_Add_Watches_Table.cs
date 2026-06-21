using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Add_Watches_Table : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "watches",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                style = table.Column<int>(type: "integer", nullable: false),
                movement = table.Column<int>(type: "integer", nullable: false),
                occasion = table.Column<int>(type: "integer", nullable: false),
                case_material = table.Column<int>(type: "integer", nullable: false),
                bracelet_type = table.Column<int>(type: "integer", nullable: false),
                image_url = table.Column<string>(type: "text", nullable: false),
                description = table.Column<string>(type: "text", nullable: true),
                brand = table.Column<string>(type: "text", nullable: false),
                dial_color = table.Column<string>(type: "text", nullable: false),
                case_thickness_mm = table.Column<decimal>(type: "numeric", nullable: false),
                lug_to_lug_mm = table.Column<decimal>(type: "numeric", nullable: false),
                lug_width_mm = table.Column<decimal>(type: "numeric", nullable: false),
                case_diameter_mm = table.Column<decimal>(type: "numeric", nullable: false),
                model = table.Column<string>(type: "text", nullable: false),
                price_eur = table.Column<decimal>(type: "numeric", nullable: false),
                reference_number = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
                table.PrimaryKey("pk_watches", x => x.id));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "watches",
            schema: "public");
    }
}

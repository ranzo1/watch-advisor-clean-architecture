using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Add_Collection_Table : Migration
{
    private static readonly string[] CollectionIndexColumns = ["user_id", "watch_id"];

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "user_collections",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                watch_id = table.Column<Guid>(type: "uuid", nullable: false),
                notes = table.Column<string>(type: "text", nullable: true),
                added_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_user_collections", x => x.id));

        migrationBuilder.CreateIndex(
            name: "ix_user_collections_user_id_watch_id",
            schema: "public",
            table: "user_collections",
            columns: CollectionIndexColumns,
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "user_collections",
            schema: "public");
    }
}

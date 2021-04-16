using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyBank.Migrations.Migrations
{
    public partial class RemoveCardHolder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardHolder",
                schema: "model",
                table: "Card");

            //migrationBuilder.RenameTable(
            //    name: "AccountCard",
            //    newName: "AccountCard",
            //    newSchema: "model");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameTable(
            //    name: "AccountCard",
            //    schema: "model",
            //    newName: "AccountCard");

            migrationBuilder.AddColumn<string>(
                name: "CardHolder",
                schema: "model",
                table: "Card",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

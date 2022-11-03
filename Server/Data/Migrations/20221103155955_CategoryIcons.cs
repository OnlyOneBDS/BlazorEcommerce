using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorEcommerce.Server.Data.Migrations;

public partial class CategoryIcons : Migration
{
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.AddColumn<string>(
      name: "Icon",
      table: "Categories",
      type: "nvarchar(max)",
      nullable: false,
      defaultValue: "");

    migrationBuilder.UpdateData(
      table: "Categories",
      keyColumn: "Id",
      keyValue: 1,
      column: "Icon",
      value: "oi oi-book");

    migrationBuilder.UpdateData(
      table: "Categories",
      keyColumn: "Id",
      keyValue: 2,
      column: "Icon",
      value: "oi oi-bullhorn");

    migrationBuilder.UpdateData(
      table: "Categories",
      keyColumn: "Id",
      keyValue: 3,
      column: "Icon",
      value: "oi oi-game-controller");
  }

  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropColumn(
      name: "Icon",
      table: "Categories");
  }
}
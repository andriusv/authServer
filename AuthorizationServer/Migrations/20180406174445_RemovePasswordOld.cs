using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AuthorizationServer.Migrations
{
    public partial class RemovePasswordOld : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordOld1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PasswordOld2",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PasswordOld3",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PasswordOld4",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordOld1",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordOld2",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordOld3",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordOld4",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}

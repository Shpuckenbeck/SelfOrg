using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SelfOrg.Data.Migrations
{
    public partial class postadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categories_CategoryId",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categories_CategoryId",
                table: "Posts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categories_CategoryId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categories_CategoryId",
                table: "Posts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

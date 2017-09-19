using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SelfOrg.Data.Migrations
{
    public partial class floatrat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "rating",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "rating",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}

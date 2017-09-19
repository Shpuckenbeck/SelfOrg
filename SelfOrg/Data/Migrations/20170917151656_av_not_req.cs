using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SelfOrg.Data.Migrations
{
    public partial class av_not_req : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Avatar",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(byte[]));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Avatar",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }
    }
}

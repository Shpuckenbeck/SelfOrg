using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SelfOrg.Data.Migrations
{
    public partial class upl_rew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Pics",
                columns: table => new
                {
                    PicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pics", x => x.PicId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pics");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Avatar",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

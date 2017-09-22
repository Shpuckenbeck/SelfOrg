using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SelfOrg.Data.Migrations
{
    public partial class amiretarded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_PostTags_Tags_TagId1",
            //    table: "PostTags");

            //migrationBuilder.DropIndex(
            //    name: "IX_PostTags_TagId1",
            //    table: "PostTags");

            //migrationBuilder.DropColumn(
            //    name: "TagId1",
            //    table: "PostTags");

            //migrationBuilder.AlterColumn<int>(
            //    name: "TagId",
            //    table: "PostTags",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldNullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_PostTags_TagId",
            //    table: "PostTags",
            //    column: "TagId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PostTags_Tags_TagId",
            //    table: "PostTags",
            //    column: "TagId",
            //    principalTable: "Tags",
            //    principalColumn: "TagId",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_PostTags_Tags_TagId",
            //    table: "PostTags");

            //migrationBuilder.DropIndex(
            //    name: "IX_PostTags_TagId",
            //    table: "PostTags");

            //migrationBuilder.AlterColumn<string>(
            //    name: "TagId",
            //    table: "PostTags",
            //    nullable: true,
            //    oldClrType: typeof(int));

            //migrationBuilder.AddColumn<int>(
            //    name: "TagId1",
            //    table: "PostTags",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_PostTags_TagId1",
            //    table: "PostTags",
            //    column: "TagId1");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PostTags_Tags_TagId1",
            //    table: "PostTags",
            //    column: "TagId1",
            //    principalTable: "Tags",
            //    principalColumn: "TagId",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}

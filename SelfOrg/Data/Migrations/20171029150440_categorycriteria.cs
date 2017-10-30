using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SelfOrg.Data.Migrations
{
    public partial class categorycriteria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatCrits",
                columns: table => new
                {
                    CatCritId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    CriterionId = table.Column<int>(nullable: false),
                    prio = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatCrits", x => x.CatCritId);
                    table.ForeignKey(
                        name: "FK_CatCrits_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatCrits_Criteria_CriterionId",
                        column: x => x.CriterionId,
                        principalTable: "Criteria",
                        principalColumn: "CriterionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatCrits_CategoryId",
                table: "CatCrits",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CatCrits_CriterionId",
                table: "CatCrits",
                column: "CriterionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatCrits");
        }
    }
}

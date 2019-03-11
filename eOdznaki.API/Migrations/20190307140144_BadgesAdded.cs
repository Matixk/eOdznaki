using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eOdznaki.Migrations
{
    public partial class BadgesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BadgeLevel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BadgeLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Badges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Acquired = table.Column<DateTime>(nullable: false),
                    BadgeStatus = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    BadgeLevelId = table.Column<int>(nullable: true),
                    BadgeTrails_BadgeLevelId = table.Column<int>(nullable: true),
                    PointsAquired = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Badges_BadgeLevel_BadgeLevelId",
                        column: x => x.BadgeLevelId,
                        principalTable: "BadgeLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Badges_BadgeLevel_BadgeTrails_BadgeLevelId",
                        column: x => x.BadgeTrails_BadgeLevelId,
                        principalTable: "BadgeLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BadgeSummitId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Badges_BadgeSummitId",
                        column: x => x.BadgeSummitId,
                        principalTable: "Badges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BadgeTrailsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trail_Badges_BadgeTrailsId",
                        column: x => x.BadgeTrailsId,
                        principalTable: "Badges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Badges_BadgeLevelId",
                table: "Badges",
                column: "BadgeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Badges_BadgeTrails_BadgeLevelId",
                table: "Badges",
                column: "BadgeTrails_BadgeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_BadgeSummitId",
                table: "Location",
                column: "BadgeSummitId");

            migrationBuilder.CreateIndex(
                name: "IX_Trail_BadgeTrailsId",
                table: "Trail",
                column: "BadgeTrailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Trail");

            migrationBuilder.DropTable(
                name: "Badges");

            migrationBuilder.DropTable(
                name: "BadgeLevel");
        }
    }
}

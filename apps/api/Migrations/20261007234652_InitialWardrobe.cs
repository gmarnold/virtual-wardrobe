using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VirtualWardrobe.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialWardrobe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Garments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Category = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Subtype = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Brand = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    Size = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    Notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Occasion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occasion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StyleTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StyleTag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GarmentColor",
                columns: table => new
                {
                    ColorId = table.Column<int>(type: "integer", nullable: false),
                    GarmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentColor", x => new { x.ColorId, x.GarmentId });
                    table.ForeignKey(
                        name: "FK_GarmentColor_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GarmentColor_Garments_GarmentId",
                        column: x => x.GarmentId,
                        principalTable: "Garments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentOccasion",
                columns: table => new
                {
                    GarmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    OccasionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentOccasion", x => new { x.GarmentId, x.OccasionId });
                    table.ForeignKey(
                        name: "FK_GarmentOccasion_Garments_GarmentId",
                        column: x => x.GarmentId,
                        principalTable: "Garments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GarmentOccasion_Occasion_OccasionId",
                        column: x => x.OccasionId,
                        principalTable: "Occasion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentSeason",
                columns: table => new
                {
                    GarmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    SeasonId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentSeason", x => new { x.GarmentId, x.SeasonId });
                    table.ForeignKey(
                        name: "FK_GarmentSeason_Garments_GarmentId",
                        column: x => x.GarmentId,
                        principalTable: "Garments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GarmentSeason_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarmentStyleTag",
                columns: table => new
                {
                    GarmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    StyleTagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentStyleTag", x => new { x.GarmentId, x.StyleTagId });
                    table.ForeignKey(
                        name: "FK_GarmentStyleTag_Garments_GarmentId",
                        column: x => x.GarmentId,
                        principalTable: "Garments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GarmentStyleTag_StyleTag_StyleTagId",
                        column: x => x.StyleTagId,
                        principalTable: "StyleTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Lavender" },
                    { 2, "Cream" },
                    { 3, "Brown" },
                    { 4, "Blue" },
                    { 5, "Mauve" },
                    { 6, "Sage" },
                    { 7, "Black" },
                    { 8, "Burgundy" },
                    { 9, "Pink" }
                });

            migrationBuilder.InsertData(
                table: "Garments",
                columns: new[] { "Id", "Brand", "Category", "CreatedAt", "Name", "Notes", "Size", "Status", "Subtype", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-4000-8000-000000000001"), "Demo Atelier", "Top", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Lavender Cardigan", "Fictional demo garment.", null, "Active", "Cardigan", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-4000-8000-000000000002"), "Demo Atelier", "Top", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Cream Lace Blouse", "Fictional demo garment.", null, "Active", "Blouse", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-4000-8000-000000000003"), "Demo Atelier", "Bottom", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Brown Flare Trousers", "Fictional demo garment.", null, "Active", "Trousers", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-4000-8000-000000000004"), "Demo Atelier", "Bottom", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Blue Midi Skirt", "Fictional demo garment.", null, "Active", "Skirt", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-4000-8000-000000000005"), "Demo Atelier", "Top", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Mauve Button-Up", "Fictional demo garment.", null, "Active", "Shirt", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-4000-8000-000000000006"), "Demo Atelier", "Dress", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Sage Wrap Dress", "Fictional demo garment.", null, "Active", "Wrap dress", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-4000-8000-000000000007"), "Demo Atelier", "Shoes", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Black Ankle Boots", "Fictional demo garment.", null, "Active", "Boots", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-4000-8000-000000000008"), "Demo Atelier", "Shoes", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Cream Sneakers", "Fictional demo garment.", null, "Active", "Sneakers", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-4000-8000-000000000009"), "Demo Atelier", "Outerwear", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Burgundy Coat", "Fictional demo garment.", null, "Active", "Coat", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) },
                    { new Guid("10000000-0000-4000-8000-000000000010"), "Demo Atelier", "Dress", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Floral Sundress", "Fictional demo garment.", null, "Active", "Sundress", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Occasion",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Casual" },
                    { 2, "Work" },
                    { 3, "Evening" },
                    { 4, "Weekend" }
                });

            migrationBuilder.InsertData(
                table: "Season",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Spring" },
                    { 2, "Summer" },
                    { 3, "Fall" },
                    { 4, "Winter" }
                });

            migrationBuilder.InsertData(
                table: "StyleTag",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Romantic" },
                    { 2, "Cozy" },
                    { 3, "Classic" },
                    { 4, "Vintage" },
                    { 5, "Minimal" },
                    { 6, "Playful" }
                });

            migrationBuilder.InsertData(
                table: "GarmentColor",
                columns: new[] { "ColorId", "GarmentId" },
                values: new object[,]
                {
                    { 1, new Guid("10000000-0000-4000-8000-000000000001") },
                    { 2, new Guid("10000000-0000-4000-8000-000000000002") },
                    { 2, new Guid("10000000-0000-4000-8000-000000000008") },
                    { 2, new Guid("10000000-0000-4000-8000-000000000010") },
                    { 3, new Guid("10000000-0000-4000-8000-000000000003") },
                    { 4, new Guid("10000000-0000-4000-8000-000000000004") },
                    { 5, new Guid("10000000-0000-4000-8000-000000000005") },
                    { 6, new Guid("10000000-0000-4000-8000-000000000006") },
                    { 7, new Guid("10000000-0000-4000-8000-000000000007") },
                    { 8, new Guid("10000000-0000-4000-8000-000000000009") },
                    { 9, new Guid("10000000-0000-4000-8000-000000000010") }
                });

            migrationBuilder.InsertData(
                table: "GarmentOccasion",
                columns: new[] { "GarmentId", "OccasionId" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-4000-8000-000000000001"), 1 },
                    { new Guid("10000000-0000-4000-8000-000000000001"), 2 },
                    { new Guid("10000000-0000-4000-8000-000000000002"), 2 },
                    { new Guid("10000000-0000-4000-8000-000000000002"), 3 },
                    { new Guid("10000000-0000-4000-8000-000000000003"), 1 },
                    { new Guid("10000000-0000-4000-8000-000000000003"), 2 },
                    { new Guid("10000000-0000-4000-8000-000000000004"), 2 },
                    { new Guid("10000000-0000-4000-8000-000000000004"), 4 },
                    { new Guid("10000000-0000-4000-8000-000000000005"), 2 },
                    { new Guid("10000000-0000-4000-8000-000000000006"), 2 },
                    { new Guid("10000000-0000-4000-8000-000000000006"), 3 },
                    { new Guid("10000000-0000-4000-8000-000000000007"), 1 },
                    { new Guid("10000000-0000-4000-8000-000000000007"), 3 },
                    { new Guid("10000000-0000-4000-8000-000000000008"), 1 },
                    { new Guid("10000000-0000-4000-8000-000000000008"), 4 },
                    { new Guid("10000000-0000-4000-8000-000000000009"), 2 },
                    { new Guid("10000000-0000-4000-8000-000000000009"), 3 },
                    { new Guid("10000000-0000-4000-8000-000000000010"), 1 },
                    { new Guid("10000000-0000-4000-8000-000000000010"), 4 }
                });

            migrationBuilder.InsertData(
                table: "GarmentSeason",
                columns: new[] { "GarmentId", "SeasonId" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-4000-8000-000000000001"), 1 },
                    { new Guid("10000000-0000-4000-8000-000000000001"), 3 },
                    { new Guid("10000000-0000-4000-8000-000000000002"), 1 },
                    { new Guid("10000000-0000-4000-8000-000000000002"), 2 },
                    { new Guid("10000000-0000-4000-8000-000000000003"), 3 },
                    { new Guid("10000000-0000-4000-8000-000000000003"), 4 },
                    { new Guid("10000000-0000-4000-8000-000000000004"), 1 },
                    { new Guid("10000000-0000-4000-8000-000000000004"), 2 },
                    { new Guid("10000000-0000-4000-8000-000000000005"), 1 },
                    { new Guid("10000000-0000-4000-8000-000000000005"), 3 },
                    { new Guid("10000000-0000-4000-8000-000000000006"), 1 },
                    { new Guid("10000000-0000-4000-8000-000000000006"), 2 },
                    { new Guid("10000000-0000-4000-8000-000000000007"), 3 },
                    { new Guid("10000000-0000-4000-8000-000000000007"), 4 },
                    { new Guid("10000000-0000-4000-8000-000000000008"), 1 },
                    { new Guid("10000000-0000-4000-8000-000000000008"), 2 },
                    { new Guid("10000000-0000-4000-8000-000000000008"), 3 },
                    { new Guid("10000000-0000-4000-8000-000000000009"), 3 },
                    { new Guid("10000000-0000-4000-8000-000000000009"), 4 },
                    { new Guid("10000000-0000-4000-8000-000000000010"), 1 },
                    { new Guid("10000000-0000-4000-8000-000000000010"), 2 }
                });

            migrationBuilder.InsertData(
                table: "GarmentStyleTag",
                columns: new[] { "GarmentId", "StyleTagId" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-4000-8000-000000000001"), 1 },
                    { new Guid("10000000-0000-4000-8000-000000000001"), 2 },
                    { new Guid("10000000-0000-4000-8000-000000000002"), 1 },
                    { new Guid("10000000-0000-4000-8000-000000000002"), 4 },
                    { new Guid("10000000-0000-4000-8000-000000000003"), 3 },
                    { new Guid("10000000-0000-4000-8000-000000000003"), 4 },
                    { new Guid("10000000-0000-4000-8000-000000000004"), 3 },
                    { new Guid("10000000-0000-4000-8000-000000000004"), 6 },
                    { new Guid("10000000-0000-4000-8000-000000000005"), 3 },
                    { new Guid("10000000-0000-4000-8000-000000000005"), 5 },
                    { new Guid("10000000-0000-4000-8000-000000000006"), 1 },
                    { new Guid("10000000-0000-4000-8000-000000000006"), 5 },
                    { new Guid("10000000-0000-4000-8000-000000000007"), 3 },
                    { new Guid("10000000-0000-4000-8000-000000000007"), 5 },
                    { new Guid("10000000-0000-4000-8000-000000000008"), 5 },
                    { new Guid("10000000-0000-4000-8000-000000000009"), 3 },
                    { new Guid("10000000-0000-4000-8000-000000000009"), 4 },
                    { new Guid("10000000-0000-4000-8000-000000000010"), 1 },
                    { new Guid("10000000-0000-4000-8000-000000000010"), 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Color_Name",
                table: "Color",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GarmentColor_GarmentId",
                table: "GarmentColor",
                column: "GarmentId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentOccasion_OccasionId",
                table: "GarmentOccasion",
                column: "OccasionId");

            migrationBuilder.CreateIndex(
                name: "IX_Garments_Category",
                table: "Garments",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentSeason_SeasonId",
                table: "GarmentSeason",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_GarmentStyleTag_StyleTagId",
                table: "GarmentStyleTag",
                column: "StyleTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Occasion_Name",
                table: "Occasion",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Season_Name",
                table: "Season",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StyleTag_Name",
                table: "StyleTag",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GarmentColor");

            migrationBuilder.DropTable(
                name: "GarmentOccasion");

            migrationBuilder.DropTable(
                name: "GarmentSeason");

            migrationBuilder.DropTable(
                name: "GarmentStyleTag");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Occasion");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "Garments");

            migrationBuilder.DropTable(
                name: "StyleTag");
        }
    }
}

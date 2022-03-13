using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BOL.Migrations
{
    public partial class createproductcategorybrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(12)", unicode: false, maxLength: 12, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Slug = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    FullDescription = table.Column<string>(type: "ntext", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", unicode: false, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", unicode: false, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: true),
                    Order = table.Column<int>(type: "int", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(12)", unicode: false, maxLength: 12, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Slug = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    FullDescription = table.Column<string>(type: "ntext", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", unicode: false, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", unicode: false, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: true),
                    Order = table.Column<int>(type: "int", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(12)", unicode: false, maxLength: 12, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Slug = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", unicode: false, precision: 18, scale: 2, nullable: false),
                    PriceDiscount = table.Column<decimal>(type: "decimal(18,2)", unicode: false, precision: 18, scale: 2, nullable: true),
                    FullDescription = table.Column<string>(type: "ntext", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BrandId = table.Column<string>(type: "char(12)", unicode: false, maxLength: 12, nullable: false),
                    Quantity = table.Column<int>(type: "int", unicode: false, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", unicode: false, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", unicode: false, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Category_Product",
                columns: table => new
                {
                    CategoryId = table.Column<string>(type: "char(12)", nullable: false),
                    ProductId = table.Column<string>(type: "char(12)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_Product", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_Category_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_Product_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_Product_CategoryId",
                table: "Category_Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                table: "Product",
                column: "BrandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category_Product");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Brand");
        }
    }
}

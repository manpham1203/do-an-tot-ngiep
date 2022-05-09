using Microsoft.EntityFrameworkCore.Migrations;

namespace BO.Migrations
{
    public partial class CreateStoredProc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                
                Create or alter Procedure MostBought
                as
                    begin
                        select p.id, p.name, p.price, p.priceDiscount, p.slug, p.BrandId
                        from Product p inner join orderdetail o on p.Id = o.ProductId 
                        group by p.id, p.name, p.price, p.priceDiscount, p.slug, p.BrandId
                        having sum(o.quantity)>10
                    end
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

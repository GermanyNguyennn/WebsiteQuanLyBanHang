namespace WebsiteQuanLyBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_18052025_1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Table_ProductImage", "ProductCategories_Id", "dbo.Table_ProductCategory");
            DropIndex("dbo.Table_ProductImage", new[] { "ProductCategories_Id" });
            DropColumn("dbo.Table_ProductImage", "ProductCategories_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Table_ProductImage", "ProductCategories_Id", c => c.Int());
            CreateIndex("dbo.Table_ProductImage", "ProductCategories_Id");
            AddForeignKey("dbo.Table_ProductImage", "ProductCategories_Id", "dbo.Table_ProductCategory", "Id");
        }
    }
}

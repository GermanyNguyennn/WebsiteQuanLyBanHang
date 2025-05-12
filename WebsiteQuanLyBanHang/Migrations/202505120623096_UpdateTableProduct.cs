namespace WebsiteQuanLyBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableProduct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Table_Product", "Description", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Table_Product", "Description", c => c.String(maxLength: 500));
        }
    }
}

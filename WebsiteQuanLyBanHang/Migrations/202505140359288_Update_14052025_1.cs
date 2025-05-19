namespace WebsiteQuanLyBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_14052025_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Table_Product", "IsNew", c => c.Boolean(nullable: false));
            DropColumn("dbo.Table_Product", "IsFeature");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Table_Product", "IsFeature", c => c.Boolean(nullable: false));
            DropColumn("dbo.Table_Product", "IsNew");
        }
    }
}

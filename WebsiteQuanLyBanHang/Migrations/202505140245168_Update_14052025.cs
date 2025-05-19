namespace WebsiteQuanLyBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_14052025 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Table_News", "SeoTitle", c => c.String());
            AlterColumn("dbo.Table_News", "SeoDescription", c => c.String());
            AlterColumn("dbo.Table_News", "SeoKeyWords", c => c.String());
            AlterColumn("dbo.Table_News", "Detail", c => c.String());
            AlterColumn("dbo.Table_Posts", "Title", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Table_Posts", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Table_News", "Detail", c => c.String(maxLength: 500));
            AlterColumn("dbo.Table_News", "SeoKeyWords", c => c.String(maxLength: 50));
            AlterColumn("dbo.Table_News", "SeoDescription", c => c.String(maxLength: 50));
            AlterColumn("dbo.Table_News", "SeoTitle", c => c.String(maxLength: 50));
        }
    }
}

namespace WebsiteQuanLyBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTablePosts : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Table_Posts", "Title", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Table_Posts", "Title", c => c.String(nullable: false, maxLength: 50));
        }
    }
}

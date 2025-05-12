namespace WebsiteQuanLyBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Table_Adv",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Alias = c.String(),
                        Description = c.String(maxLength: 500),
                        Image = c.String(maxLength: 500),
                        Type = c.Int(nullable: false),
                        Link = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Table_Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Alias = c.String(),
                        Link = c.String(maxLength: 500),
                        Description = c.String(maxLength: 500),
                        Position = c.Int(nullable: false),
                        SeoTitle = c.String(maxLength: 50),
                        SeoDescription = c.String(maxLength: 50),
                        SeoKeyWords = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Table_News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 500),
                        Alias = c.String(maxLength: 500),
                        CategoryId = c.Int(nullable: false),
                        Description = c.String(maxLength: 500),
                        SeoTitle = c.String(maxLength: 50),
                        SeoDescription = c.String(maxLength: 50),
                        SeoKeyWords = c.String(maxLength: 50),
                        Detail = c.String(maxLength: 500),
                        Image = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Table_Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Table_Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Alias = c.String(),
                        CategoryId = c.Int(nullable: false),
                        Description = c.String(maxLength: 500),
                        SeoTitle = c.String(),
                        SeoDescription = c.String(),
                        SeoKeyWords = c.String(),
                        Detail = c.String(),
                        Image = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Table_Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Table_Contact",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Alias = c.String(maxLength: 500),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(maxLength: 500),
                        Website = c.String(maxLength: 500),
                        Message = c.String(maxLength: 500),
                        IsRead = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Table_OrderDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Alias = c.String(maxLength: 50),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Table_Order", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Table_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Table_Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Alias = c.String(maxLength: 50),
                        Code = c.String(nullable: false, maxLength: 50),
                        CustomerName = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 500),
                        Email = c.String(nullable: false, maxLength: 500),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        TypePayment = c.Int(nullable: false),
                        CustomerId = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Table_Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Alias = c.String(),
                        ProductCode = c.String(),
                        ProductCategoryId = c.Int(nullable: false),
                        Description = c.String(maxLength: 500),
                        SeoTitle = c.String(),
                        SeoDescription = c.String(),
                        SeoKeyWords = c.String(),
                        Detail = c.String(),
                        Image = c.String(),
                        OriginalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceSale = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsHome = c.Boolean(nullable: false),
                        IsSale = c.Boolean(nullable: false),
                        IsFeature = c.Boolean(nullable: false),
                        IsHot = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Table_ProductCategory", t => t.ProductCategoryId, cascadeDelete: true)
                .Index(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.Table_ProductCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Alias = c.String(maxLength: 50),
                        Title = c.String(nullable: false, maxLength: 500),
                        Description = c.String(maxLength: 500),
                        Icon = c.String(),
                        SeoTitle = c.String(maxLength: 50),
                        SeoDescription = c.String(maxLength: 50),
                        SeoKeyWords = c.String(maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Table_ProductImage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Alias = c.String(maxLength: 50),
                        ProductId = c.Int(nullable: false),
                        Image = c.String(maxLength: 500),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Table_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Table_ReviewProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        UserName = c.String(),
                        FullName = c.String(),
                        Email = c.String(),
                        Content = c.String(),
                        Rate = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Avatar = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Table_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Table_Wishlist",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        UserName = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Table_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Table_Subscribe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Alias = c.String(maxLength: 50),
                        Email = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Table_SystemSetting",
                c => new
                    {
                        SettingKey = c.String(nullable: false, maxLength: 50),
                        SettingValue = c.String(maxLength: 100),
                        SettingDescription = c.String(maxLength: 100),
                        Alias = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.SettingKey);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Phone = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Table_Wishlist", "ProductId", "dbo.Table_Product");
            DropForeignKey("dbo.Table_ReviewProduct", "ProductId", "dbo.Table_Product");
            DropForeignKey("dbo.Table_ProductImage", "ProductId", "dbo.Table_Product");
            DropForeignKey("dbo.Table_Product", "ProductCategoryId", "dbo.Table_ProductCategory");
            DropForeignKey("dbo.Table_OrderDetail", "ProductId", "dbo.Table_Product");
            DropForeignKey("dbo.Table_OrderDetail", "OrderId", "dbo.Table_Order");
            DropForeignKey("dbo.Table_Posts", "CategoryId", "dbo.Table_Category");
            DropForeignKey("dbo.Table_News", "CategoryId", "dbo.Table_Category");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Table_Wishlist", new[] { "ProductId" });
            DropIndex("dbo.Table_ReviewProduct", new[] { "ProductId" });
            DropIndex("dbo.Table_ProductImage", new[] { "ProductId" });
            DropIndex("dbo.Table_Product", new[] { "ProductCategoryId" });
            DropIndex("dbo.Table_OrderDetail", new[] { "ProductId" });
            DropIndex("dbo.Table_OrderDetail", new[] { "OrderId" });
            DropIndex("dbo.Table_Posts", new[] { "CategoryId" });
            DropIndex("dbo.Table_News", new[] { "CategoryId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Table_SystemSetting");
            DropTable("dbo.Table_Subscribe");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Table_Wishlist");
            DropTable("dbo.Table_ReviewProduct");
            DropTable("dbo.Table_ProductImage");
            DropTable("dbo.Table_ProductCategory");
            DropTable("dbo.Table_Product");
            DropTable("dbo.Table_Order");
            DropTable("dbo.Table_OrderDetail");
            DropTable("dbo.Table_Contact");
            DropTable("dbo.Table_Posts");
            DropTable("dbo.Table_News");
            DropTable("dbo.Table_Category");
            DropTable("dbo.Table_Adv");
        }
    }
}

namespace ArtProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Painter",
                c => new
                    {
                        PainterID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PictureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PainterID);
            
            CreateTable(
                "dbo.Picture",
                c => new
                    {
                        PictureID = c.Int(nullable: false, identity: true),
                        PainterID = c.Int(nullable: false),
                        Title = c.String(),
                        RealiseDate = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PictureID)
                .ForeignKey("dbo.Painter", t => t.PainterID, cascadeDelete: true)
                .Index(t => t.PainterID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Picture", "PainterID", "dbo.Painter");
            DropIndex("dbo.Picture", new[] { "PainterID" });
            DropTable("dbo.Picture");
            DropTable("dbo.Painter");
        }
    }
}

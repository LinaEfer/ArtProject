namespace ArtProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialChanges : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Painter", "PictureID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Painter", "PictureID", c => c.Int(nullable: false));
        }
    }
}

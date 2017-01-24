namespace ArtProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaxLengthOnTitle : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Picture", "Title", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Picture", "Title", c => c.String());
        }
    }
}

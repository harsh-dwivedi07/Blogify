namespace BlogsAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ImageData", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "ImageData");
        }
    }
}

namespace BlogsAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedPostTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "AuthorId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "AuthorId");
        }
    }
}

namespace BlogsAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Author = c.String(),
                        Content = c.String(),
                        Status = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        PublishedOn = c.DateTime(nullable: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Posts");
        }
    }
}

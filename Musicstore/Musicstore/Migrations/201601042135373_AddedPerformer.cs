namespace Musicstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPerformer : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Song", name: "Publisher_Id", newName: "PublisherId");
            RenameIndex(table: "dbo.Song", name: "IX_Publisher_Id", newName: "IX_PublisherId");
            CreateTable(
                "dbo.Performers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Song", "PerformerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Song", "PerformerId");
            AddForeignKey("dbo.Song", "PerformerId", "dbo.Performers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Song", "PerformerId", "dbo.Performers");
            DropIndex("dbo.Song", new[] { "PerformerId" });
            DropColumn("dbo.Song", "PerformerId");
            DropTable("dbo.Performers");
            RenameIndex(table: "dbo.Song", name: "IX_PublisherId", newName: "IX_Publisher_Id");
            RenameColumn(table: "dbo.Song", name: "PublisherId", newName: "Publisher_Id");
        }
    }
}

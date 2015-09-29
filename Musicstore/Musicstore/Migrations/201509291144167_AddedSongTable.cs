namespace Musicstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSongTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Length = c.String(),
                        Year = c.DateTime(nullable: false),
                        Genre = c.String(),
                        Publisher_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publisher", t => t.Publisher_Id)
                .Index(t => t.Publisher_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Songs", "Publisher_Id", "dbo.Publisher");
            DropIndex("dbo.Songs", new[] { "Publisher_Id" });
            DropTable("dbo.Songs");
        }
    }
}

namespace Musicstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPublisherTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PublisherModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PublisherModels");
        }
    }
}

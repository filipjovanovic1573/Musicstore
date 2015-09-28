namespace Musicstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedPublisherTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PublisherModels", newName: "Publisher");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Publisher", newName: "PublisherModels");
        }
    }
}

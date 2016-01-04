namespace Musicstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPerformerRenamed : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Performers", newName: "Performer");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Performer", newName: "Performers");
        }
    }
}

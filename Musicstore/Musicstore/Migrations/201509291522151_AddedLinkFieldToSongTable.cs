namespace Musicstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLinkFieldToSongTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Song", "Link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Song", "Link");
        }
    }
}

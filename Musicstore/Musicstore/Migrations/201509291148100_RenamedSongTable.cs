namespace Musicstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedSongTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Songs", newName: "Song");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Song", newName: "Songs");
        }
    }
}

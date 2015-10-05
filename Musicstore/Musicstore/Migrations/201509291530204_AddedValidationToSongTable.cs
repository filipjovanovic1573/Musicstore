namespace Musicstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedValidationToSongTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Song", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Song", "Length", c => c.String(nullable: false));
            AlterColumn("dbo.Song", "Genre", c => c.String(nullable: false));
            AlterColumn("dbo.Song", "Link", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Song", "Link", c => c.String());
            AlterColumn("dbo.Song", "Genre", c => c.String());
            AlterColumn("dbo.Song", "Length", c => c.String());
            AlterColumn("dbo.Song", "Name", c => c.String());
        }
    }
}

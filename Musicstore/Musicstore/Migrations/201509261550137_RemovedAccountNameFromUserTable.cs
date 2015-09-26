namespace Musicstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedAccountNameFromUserTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "AccountName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "AccountName", c => c.String());
        }
    }
}

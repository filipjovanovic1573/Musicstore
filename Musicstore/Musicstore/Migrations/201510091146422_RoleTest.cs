namespace Musicstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleTest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Role", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Role", "Discriminator");
        }
    }
}

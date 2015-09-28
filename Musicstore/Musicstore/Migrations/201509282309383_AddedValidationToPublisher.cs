namespace Musicstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedValidationToPublisher : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Publisher", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Publisher", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Publisher", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Publisher", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Publisher", "Address", c => c.String());
            AlterColumn("dbo.Publisher", "Phone", c => c.String());
            AlterColumn("dbo.Publisher", "Email", c => c.String());
            AlterColumn("dbo.Publisher", "Name", c => c.String());
        }
    }
}

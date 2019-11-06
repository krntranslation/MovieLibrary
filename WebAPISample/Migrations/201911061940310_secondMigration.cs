namespace WebAPISample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Director", c => c.String());
            DropColumn("dbo.Movies", "DirectorName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "DirectorName", c => c.String());
            DropColumn("dbo.Movies", "Director");
        }
    }
}

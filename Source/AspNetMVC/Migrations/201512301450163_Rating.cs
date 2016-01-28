using System;
using System.Data.Entity.Migrations;

namespace AspNetMVC.Migrations
{
    public partial class Rating : DbMigration
    {
        public override void Down()
        {
            DropColumn("dbo.Movies", "Rating");
        }

        public override void Up()
        {
            AddColumn("dbo.Movies", "Rating", c => c.String());
        }
    }
}

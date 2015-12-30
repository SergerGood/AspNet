using System;
using System.Data.Entity.Migrations;

namespace AspNetMVC.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Down()
        {
            DropTable("dbo.Movies");
        }

        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Genre = c.String(),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    ReleaseDate = c.DateTime(nullable: false),
                    Title = c.String(),
                })
                .PrimaryKey(t => t.ID);
        }
    }
}

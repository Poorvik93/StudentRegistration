namespace StudentRegistarationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addingtheusertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 100, unicode: false),
                        Password = c.String(),
                        Email = c.String(),
                        IsAcceptedTerms = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}

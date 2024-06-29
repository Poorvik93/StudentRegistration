namespace StudentRegistarationPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingtheSelectedCoursetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SelectedCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SelectedCourses", "UserId", "dbo.Users");
            DropForeignKey("dbo.SelectedCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.SelectedCourses", new[] { "CourseId" });
            DropIndex("dbo.SelectedCourses", new[] { "UserId" });
            DropTable("dbo.SelectedCourses");
        }
    }
}

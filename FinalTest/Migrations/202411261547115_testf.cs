namespace FinalTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addmissions",
                c => new
                    {
                        AdmitId = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdmitId)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentName = c.String(),
                        Age = c.Int(nullable: false),
                        IsAdmitted = c.Boolean(nullable: false),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectId = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(),
                    })
                .PrimaryKey(t => t.SubjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addmissions", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Addmissions", "StudentId", "dbo.Students");
            DropIndex("dbo.Addmissions", new[] { "StudentId" });
            DropIndex("dbo.Addmissions", new[] { "SubjectId" });
            DropTable("dbo.Subjects");
            DropTable("dbo.Students");
            DropTable("dbo.Addmissions");
        }
    }
}

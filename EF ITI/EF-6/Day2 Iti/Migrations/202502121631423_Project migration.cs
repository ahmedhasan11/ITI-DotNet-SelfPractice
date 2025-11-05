namespace Day2_Iti.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Projectmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Department_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("HR.Department", t => t.Department_Id)
                .Index(t => t.Department_Id);
            
            AddColumn("dbo.Employees", "DepartmentID", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "Date", c => c.DateTime(nullable: false, storeType: "date"));
            CreateIndex("dbo.Employees", "DepartmentID");
            AddForeignKey("dbo.Employees", "DepartmentID", "HR.Department", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "Department_Id", "HR.Department");
            DropForeignKey("dbo.Employees", "DepartmentID", "HR.Department");
            DropIndex("dbo.Projects", new[] { "Department_Id" });
            DropIndex("dbo.Employees", new[] { "DepartmentID" });
            AlterColumn("dbo.Employees", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Employees", "DepartmentID");
            DropTable("dbo.Projects");
        }
    }
}

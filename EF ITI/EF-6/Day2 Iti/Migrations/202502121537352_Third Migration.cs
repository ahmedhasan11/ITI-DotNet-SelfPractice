namespace Day2_Iti.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Departments", newName: "Department");
            MoveTable(name: "dbo.Department", newSchema: "HR");
            RenameColumn(table: "dbo.Employees", name: "Name", newName: "FullName");
            AddColumn("HR.Department", "Location", c => c.String(nullable: false,defaultValue:""));
            AddColumn("dbo.Employees", "Date", c => c.DateTime(nullable: false));
            AlterColumn("HR.Department", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Employees", "FullName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Employees", "Salary", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Salary", c => c.Double(nullable: false));
            AlterColumn("dbo.Employees", "FullName", c => c.String());
            AlterColumn("HR.Department", "Name", c => c.String());
            DropColumn("dbo.Employees", "Date");
            DropColumn("HR.Department", "Location");
            RenameColumn(table: "dbo.Employees", name: "FullName", newName: "Name");
            MoveTable(name: "HR.Department", newSchema: "dbo");
            RenameTable(name: "dbo.Department", newName: "Departments");
        }
    }
}

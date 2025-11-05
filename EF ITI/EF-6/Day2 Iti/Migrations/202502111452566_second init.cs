namespace Day2_Iti.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondinit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Address");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF.NET5.Migrations
{
    public partial class atten : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Employees_EmployeeID",
                table: "Attendance",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Employees_EmployeeID",
                table: "Attendance");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeApplication.Infra.Domain.Migrations
{
    public partial class initemployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CvFile",
                table: "employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CvFile",
                table: "employees");
        }
    }
}

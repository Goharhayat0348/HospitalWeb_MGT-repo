using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Repositories.Migrations
{
    public partial class DoctorToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDoctor",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDoctor",
                table: "AspNetUsers");
        }
    }
}

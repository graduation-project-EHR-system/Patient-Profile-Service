using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalRecords.Service.Core.DbContexts.Migrations
{
    /// <inheritdoc />
    public partial class DeleteSpecilizationForDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Speciality",
                table: "CachedDoctors");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Patients",
                type: "char(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(11)",
                oldMaxLength: 11);

            migrationBuilder.AddColumn<string>(
                name: "Speciality",
                table: "CachedDoctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

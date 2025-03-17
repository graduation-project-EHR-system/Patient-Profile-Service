using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalRecords.Service.Core.DbContexts.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNationalIDcoloumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NationalID",
                table: "Patients",
                type: "char(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(11)",
                oldMaxLength: 11);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NationalID",
                table: "Patients",
                type: "char(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(14)",
                oldMaxLength: 14);
        }
    }
}

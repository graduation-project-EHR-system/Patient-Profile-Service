using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalRecords.Service.Core.DbContexts.Migrations
{
    /// <inheritdoc />
    public partial class updateColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ObservationDate",
                table: "Observations",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Observations",
                newName: "ObservationDate");
        }
    }
}

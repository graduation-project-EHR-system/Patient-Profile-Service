using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalRecords.Service.Core.DbContexts.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDoctorFromMR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_CachedDoctors_CachedDoctorId",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_CachedDoctorId",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "CachedDoctorId",
                table: "MedicalRecords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CachedDoctorId",
                table: "MedicalRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_CachedDoctorId",
                table: "MedicalRecords",
                column: "CachedDoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_CachedDoctors_CachedDoctorId",
                table: "MedicalRecords",
                column: "CachedDoctorId",
                principalTable: "CachedDoctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

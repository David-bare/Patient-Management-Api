using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelAddTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientRecords_Patients_PatientId",
                table: "PatientRecords");

            migrationBuilder.DropIndex(
                name: "IX_PatientRecords_PatientId",
                table: "PatientRecords");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "PatientRecords",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BloodGroup",
                table: "PatientRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Genotype",
                table: "PatientRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sickness",
                table: "PatientRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "BloodGroup",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "Genotype",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "Sickness",
                table: "PatientRecords");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRecords_PatientId",
                table: "PatientRecords",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRecords_Patients_PatientId",
                table: "PatientRecords",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

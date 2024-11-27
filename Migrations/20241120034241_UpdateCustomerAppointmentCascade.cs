using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NailManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerAppointmentCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Appointme__Custo__4316F928",
                table: "Appointments");

            migrationBuilder.AddForeignKey(
                name: "FK__Appointme__Custo__4316F928",
                table: "Appointments",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Appointme__Custo__4316F928",
                table: "Appointments");

            migrationBuilder.AddForeignKey(
                name: "FK__Appointme__Custo__4316F928",
                table: "Appointments",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID");
        }
    }
}

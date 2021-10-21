using Microsoft.EntityFrameworkCore.Migrations;

namespace SanyaaDelivery.Infra.Data.Migrations
{
    public partial class CleanerSubcribersEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CleaningSubscribers_client_t_ClientId",
                table: "CleaningSubscribers");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "CleaningSubscribers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SystemUserId",
                table: "CleaningSubscribers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CleaningSubscribers_SystemUserId",
                table: "CleaningSubscribers",
                column: "SystemUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CleaningSubscribers_client_t_ClientId",
                table: "CleaningSubscribers",
                column: "ClientId",
                principalTable: "client_t",
                principalColumn: "client_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CleaningSubscribers_system_user_t_SystemUserId",
                table: "CleaningSubscribers",
                column: "SystemUserId",
                principalTable: "system_user_t",
                principalColumn: "system_user_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CleaningSubscribers_client_t_ClientId",
                table: "CleaningSubscribers");

            migrationBuilder.DropForeignKey(
                name: "FK_CleaningSubscribers_system_user_t_SystemUserId",
                table: "CleaningSubscribers");

            migrationBuilder.DropIndex(
                name: "IX_CleaningSubscribers_SystemUserId",
                table: "CleaningSubscribers");

            migrationBuilder.DropColumn(
                name: "SystemUserId",
                table: "CleaningSubscribers");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "CleaningSubscribers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CleaningSubscribers_client_t_ClientId",
                table: "CleaningSubscribers",
                column: "ClientId",
                principalTable: "client_t",
                principalColumn: "client_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

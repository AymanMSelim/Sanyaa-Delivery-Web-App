using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SanyaaDelivery.Infra.Data.Migrations
{
    public partial class CleanerSubcribers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CleaningSubscribers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Package = table.Column<int>(nullable: false),
                    SubscribeDate = table.Column<DateTime>(nullable: false),
                    ClientId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CleaningSubscribers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CleaningSubscribers_client_t_ClientId",
                        column: x => x.ClientId,
                        principalTable: "client_t",
                        principalColumn: "client_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CleaningSubscribers_ClientId",
                table: "CleaningSubscribers",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CleaningSubscribers");
        }
    }
}

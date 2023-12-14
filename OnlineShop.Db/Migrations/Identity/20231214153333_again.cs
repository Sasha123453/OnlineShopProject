using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Db.Migrations.Identity
{
    /// <inheritdoc />
    public partial class again : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DeliveryInfoItem_DeliveryInfoItemId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeliveryInfoItemId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DeliveryInfoItem_DeliveryInfoItemId",
                table: "AspNetUsers",
                column: "DeliveryInfoItemId",
                principalTable: "DeliveryInfoItem",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DeliveryInfoItem_DeliveryInfoItemId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeliveryInfoItemId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DeliveryInfoItem_DeliveryInfoItemId",
                table: "AspNetUsers",
                column: "DeliveryInfoItemId",
                principalTable: "DeliveryInfoItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
